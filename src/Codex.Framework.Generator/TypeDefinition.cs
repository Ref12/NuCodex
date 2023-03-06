namespace Codex.Framework.Generator
{
    public class TypeDefinition
    {
        public TypeDefinition(Type type, GeneratorContext context)
        {
            Type = type;
            Context = context;
        }

        public Type Type { get; }
        public GeneratorContext Context { get; }
    }
}