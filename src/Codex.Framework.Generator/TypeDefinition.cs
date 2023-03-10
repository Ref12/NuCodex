using System.Collections.Immutable;
using System.Reflection;
using Meziantou.Framework.CodeDom;

namespace Codex.Framework.Generator
{
    public class TypeDefinition
    {
        public TypeDefinition(Type type, GeneratorContext context)
        {
            Type = type;
            Context = context;

            BaseName = type.Name.TrimStart('I');
            ModelDeclaration = new ClassDeclaration(BaseName)
            { 
                Modifiers = Modifiers.Public | Modifiers.Partial
            };

            IsAdapter = type.GetCustomAttribute<AdapterTypeAttribute>() != null;

            if (!IsAdapter)
            {
                context.ModelNamespace.Types.Add(ModelDeclaration);
            }
        }

        public Type Type { get; }
        public GeneratorContext Context { get; }
        public string BaseName { get; }

        public ImmutableDictionary<string, PropertyInfo> Properties { get; set; }
        public ImmutableHashSet<Type> Interfaces { get; set; }
        public ClassDeclaration ModelDeclaration { get; }
        public bool IsAdapter { get; }
        public TypeDefinition BaseDefinition { get; set; }
    }
}