using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Reflection;
using Codex.ObjectModel;
using Meziantou.Framework.CodeDom;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Options;
using Microsoft.CodeAnalysis.Simplification;
using Dom = Meziantou.Framework.CodeDom;

namespace Codex.Framework.Generator;

public record GeneratorContext(string OutputPath)
{
    public IReadOnlyDictionary<Type, TypeDefinition> Types;

    public NamespaceDeclaration ModelNamespace = new("Codex.ObjectModel.Implementation");

    public Workspace Workspace = new AdhocWorkspace();

    public void Initialize()
    {
        var assembly = typeof(SearchBehavior).Assembly;

        Types = assembly.GetTypes()
            .Where(t => t.IsInterface
                && !t.IsGenericType
                && t.GetMethods().Where(m => !m.IsSpecialName).Count() == 0)
            .Select(ToTypeDefinition)
            .ToImmutableDictionary(t => t.Type);

        CompilationUnit file = new CompilationUnit();
        file.AddNamespace(ModelNamespace);

        DiscoverBaseTypes();

        AddProperties();

        CSharpCodeGenerator generator = new CodeGenerator();
        //using var modelFileWriter = new StreamWriter(Path.Combine(OutputPath, "Model.g.cs"));
        using var modelWriter = new StringWriter();
        generator.Write(modelWriter, file);

        var tree = CSharpSyntaxTree.ParseText(modelWriter.ToString());
        PostProcessAndSave(tree);
    }

    private void PostProcessAndSave(SyntaxTree tree)
    {
        var rewriter = new PostProcessRewriter();
        SyntaxNode node = tree.GetRoot();
        node = rewriter.Visit(node.NormalizeWhitespace());
        node = Formatter.Format(node, Workspace);
        using var modelFileWriter = new StreamWriter(Path.Combine(OutputPath, "Model.g.cs"));
        node.WriteTo(modelFileWriter);
    }

    private void AddProperties()
    {
        foreach (var type in Types.Values)
        {
            AddProperties(type);
        }
    }

    private void AddProperties(TypeDefinition type)
    {
        if (type.Properties != null)
        {
            return;
        }

        if (type.BaseDefinition !=  null && type.BaseDefinition.Properties == null)
        {
            AddProperties(type.BaseDefinition);
        }

        type.Properties = type.BaseDefinition?.Properties ?? ImmutableDictionary<string, PropertyInfo>.Empty;
        var properties = type.Properties.ToBuilder();
        foreach (var property in new[] { type.Type }.Concat(type.Type.GetInterfaces()).SelectMany(t => t.GetProperties()))
        {
            if (properties.TryGetValue(property.Name, out _))
            {
                continue;
            }

            properties[property.Name] = property;
            AddProperty(type, property);
        }

        type.Properties = properties.ToImmutable();
    }

    private void AddProperty(TypeDefinition type, PropertyInfo property)
    {
        var propertyType = property.PropertyType;
        bool isList = propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(IReadOnlyList<>);
        if (isList)
        {
            propertyType = propertyType.GenericTypeArguments[0];
        }

        bool isModelType = Types.TryGetValue(propertyType, out var modelType);

        var propertyTypeReference = getPropertyTypeReference();

        TypeReference getPropertyTypeReference()
        {
            if (isList)
            {
                if (isModelType)
                {
                    return new TypeReference("List").MakeGeneric(new TypeReference(modelType.BaseName));
                }
                else
                {
                    return new TypeReference("List").MakeGeneric(new TypeReference(propertyType));
                }
            }
            else if (isModelType)
            {
                return new TypeReference(modelType.BaseName);
            }
            else
            {
                return new TypeReference(propertyType);
            }
        }

        type.ModelDeclaration.Members.Add(new InitializedPropertyDeclaration(property.Name, propertyTypeReference)
            .ApplyIf(isList, p => p.InitExpression = new NewObjectExpression())
            .AddAutoGet()
            .AddAutoSet()
            .Apply(p => p.Modifiers = Modifiers.Public));

        if (isList || isModelType)
        {
            // Need to generate explicit interface implementation since
            // signature will not match for generated property

            CodeMemberProperty interfaceImplProperty = new PropertyDeclaration(property.Name, new TypeReference(property.PropertyType))
            {
                PrivateImplementationType = new TypeReference(type.Type)
            };

            interfaceImplProperty.Getter = new PropertyAccessorDeclaration(
                new StatementCollection()
                {
                    new ReturnStatement(new MemberReferenceExpression(null, property.Name))
                });

            type.ModelDeclaration.Members.Add(interfaceImplProperty);
        }
    }

    private void DiscoverBaseTypes()
    {
        foreach (var typeDefinition in Types.Values)
        {
            var baseType = typeDefinition.Type.GetInterfaces().FirstOrDefault();
            if (baseType != null)
            {
                typeDefinition.BaseDefinition = Types.GetValueOrDefault(baseType);
                if (typeDefinition.BaseDefinition != null)
                {
                    typeDefinition.ModelDeclaration.BaseType = new TypeReference(typeDefinition.BaseDefinition.BaseName);
                }
            }

            foreach (var baseInterface in new[] { typeDefinition.Type }.Concat(typeDefinition.Type.GetInterfaces().Skip(1)))
            {
                typeDefinition.ModelDeclaration.Implements.Add(baseInterface);
            }
        }
    }

    private TypeDefinition ToTypeDefinition(Type type)
    {
        return new TypeDefinition(type, this);
    }

    private class CodeGenerator : CSharpCodeGenerator
    {
        protected override void WritePropertyDeclaration(IndentedTextWriter writer, PropertyDeclaration member)
        {
            base.WritePropertyDeclaration(writer, member);

            if (member is InitializedPropertyDeclaration init && init.InitExpression != null)
            {
                writer.Write(" = ");
                WriteExpression(writer, init.InitExpression);
                writer.WriteLine(";");
            }
        }
    }

    public class InitializedPropertyDeclaration : PropertyDeclaration
    {
        public InitializedPropertyDeclaration(string name, CodeTypeReference type) 
            : base(name, type)
        {
        }

        public Expression InitExpression { get; set; }
    }

    private class PostProcessRewriter : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitAliasQualifiedName(AliasQualifiedNameSyntax node)
        {
            return Visit(node.Name);
        }

        public override SyntaxNode VisitQualifiedName(QualifiedNameSyntax node)
        {
            return Visit(node.Right);
        }

        public override SyntaxNode VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            return node.WithMembers(VisitList(node.Members));
        }
    }
}