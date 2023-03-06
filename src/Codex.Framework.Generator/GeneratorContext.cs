using System.Collections.Concurrent;
using Codex.ObjectModel;
using Meziantou.Framework.CodeDom;

namespace Codex.Framework.Generator;

public record GeneratorContext(string OutputPath)
{
    public List<TypeDefinition> Types = new();
    public readonly ConcurrentDictionary<Type, TypeDefinition> DefinitionsByType = new();

    public NamespaceDeclaration ModelNamespace = new("Codex.ObjectModel.Implementation");

    public void Initialize()
    {
        var assembly = typeof(SearchBehavior).Assembly;

        Types = assembly.GetTypes()
            .Where(t => t.IsInterface
                && !t.IsGenericType
                && t.GetMethods().Where(m => !m.IsSpecialName).Count() == 0)
            .Select(ToTypeDefinition)
            .ToList();

        CompilationUnit file = new CompilationUnit();
        file.AddNamespace(ModelNamespace);

        CSharpCodeGenerator generator = new CSharpCodeGenerator();
        using var modelFileWriter = new StreamWriter(Path.Combine(OutputPath, "Model.g.cs"));
        generator.Write(modelFileWriter, file);
    }

    private TypeDefinition ToTypeDefinition(Type type)
    {
        return new TypeDefinition(type, this);
    }
}