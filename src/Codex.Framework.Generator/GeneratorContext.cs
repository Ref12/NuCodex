using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Reflection;
using Codex.ObjectModel;
using Codex.Utilities;
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

    public NamespaceDeclaration ModelNamespace = new("Codex.ObjectModel.Implementation")
    {
        Usings =
        {
            new Dom.UsingDirective($"static {nameof(PropertyTarget)}")
        }
    };

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

        var decl = type.ModelDeclaration;
        type.Interfaces = new[] { type.Type }.Concat(type.Type.GetInterfaces()).ToImmutableHashSet().Union(type.BaseDefinition?.Interfaces ?? ImmutableHashSet<Type>.Empty);
        type.Properties = type.BaseDefinition?.Properties ?? ImmutableDictionary<string, PropertyInfo>.Empty;

        decl.Members.Add(new ConstructorDeclaration()
        {
            Modifiers = Modifiers.Public
        });

        decl.Members.Add(new MethodDeclaration(nameof(IPropertyTarget.CreateClone))
        {
            PrivateImplementationType = typeof(IPropertyTarget),
            ReturnType = typeof(object),
            Statements = new()
            {
                new ReturnStatement(new NewObjectExpression(decl, new CastExpression(new ThisExpression(), type.Type)))
            }
        });

        foreach (var baseType in type.Interfaces)
        {
            if (Types.ContainsKey(baseType))
            {
                MethodArgumentDeclaration argument = new MethodArgumentDeclaration(baseType, "source");

                var copyMethod = new MethodDeclaration("CopyFrom")
                {
                    Modifiers = Modifiers.Public,
                    Statements = new StatementCollection()
                };

                decl.Members.Add(new ConstructorDeclaration()
                {
                    Modifiers = Modifiers.Public,
                    Arguments = { argument },
                    Statements = new()
                    {
                        new MethodInvokeExpression(copyMethod, argument)
                    }
                });

                copyMethod.AddArgument(argument);

                if (type.BaseDefinition?.Interfaces.Contains(baseType) != true)
                {
                    decl.Implements.Add(new TypeReference(typeof(IPropertyTarget<>).MakeGenericType(baseType)));
                    type.ModelDeclaration.Members.Add(copyMethod);
                }

                foreach (var property in baseType.GetProperties())
                {
                    AddProperty(type, property, out var propertyType, out bool isList, out bool isModelType, out var copyTypeRef);

                    copyMethod.Statements.Add(
                        new AssignStatement(
                            new MemberReferenceExpression(null, property.Name),
                            new MethodInvokeExpression(
                                new MemberReferenceExpression(null, nameof(PropertyTarget.GetOrCopy)),
                                new MemberReferenceExpression(null, property.Name),
                                new MemberReferenceExpression(copyMethod.Arguments[0], property.Name))
                            {
                                Parameters =
                                {
                                   copyTypeRef,
                                   propertyType
                                }
                            }));
                }
            }
        }

        decl.Members.Sort(new ComparerBuilder<MemberDeclaration>()
            .CompareByAfter(m => m is ConstructorDeclaration ? 0 : (m is MethodDeclaration ? 1 : 2)));
    }

    private void AddProperty(
        TypeDefinition type,
        PropertyInfo property,
        out Type propertyType,
        out bool isList,
        out bool isModelType,
        out TypeReference copyTypeRef)
    {
        var coerceGet = property.GetCustomAttribute<CoerceGetAttribute>();
        var isReadOnlyList = property.GetCustomAttribute<ReadOnlyListAttribute>() != null;

        propertyType = property.PropertyType;
        isList = propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(IReadOnlyList<>);
        if (isList)
        {
            propertyType = propertyType.GenericTypeArguments[0];
        }

        isModelType = Types.TryGetValue(propertyType, out var modelType);
        var propertyTypeReference = getPropertyTypeReference(propertyType, isList, isModelType);

        TypeReference getPropertyTypeReference(Type propertyType, bool isList, bool isModelType)
        {
            if (isList)
            {
                string listType = isReadOnlyList ? "IReadOnlyList" : "List";
                if (isModelType)
                {
                    return new TypeReference(listType).MakeGeneric(new TypeReference(modelType.BaseName));
                }
                else
                {
                    return new TypeReference(listType).MakeGeneric(new TypeReference(propertyType));
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

        copyTypeRef = isList ? propertyTypeReference.Parameters[0] : propertyTypeReference;

        if (type.Properties.TryGetValue(property.Name, out _))
        {
            return;
        }

        type.Properties = type.Properties.SetItem(property.Name, property);

        var propertyDeclaration = new InitializedPropertyDeclaration(property.Name, propertyTypeReference)
            .Apply(p => p.Modifiers = Modifiers.Public);
        type.ModelDeclaration.Members.Add(propertyDeclaration);

        if (coerceGet == null)
        {
            propertyDeclaration
                .ApplyIf(isList && isReadOnlyList, p => p.InitExpression = 
                    isReadOnlyList 
                    ? new MemberReferenceExpression(typeof(Array), "Empty").InvokeMethod(new[] { propertyTypeReference.Parameters[0] })
                    : new NewObjectExpression())
            .AddAutoGet()
            .AddAutoSet();
        }
        else
        {
            var field = new FieldDeclaration("m_" + property.Name, coerceGet.CoercedSourceType ?? propertyTypeReference)
                .Apply(f => f.Modifiers = Modifiers.Private);
            type.ModelDeclaration.Members.Add(field);

            // get => Coerce{PropertyName}(m_{PropertyName});
            propertyDeclaration.Getter = new ReturnStatement(
                new MethodInvokeExpression(
                    new MemberReferenceExpression(null, "Coerce" + property.Name),
                    field));

            // set => m_{PropertyName} = value;
            propertyDeclaration.Setter = new AssignStatement(
                field,
                new ValueArgumentExpression());
        }

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
            else
            {
                typeDefinition.ModelDeclaration.BaseType = typeof(EntityBase);
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