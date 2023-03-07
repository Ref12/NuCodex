using Meziantou.Framework.CodeDom;

namespace Codex.Framework.Generator
{
    public static class CodeDomHelpers
    {
        public static T Apply<T>(this T c, Action<T> apply)
            where T : CodeObject
        {
            apply(c);
            return c;
        }

        public static T ApplyIf<T>(this T c, bool shouldApply, Action<T> apply)
            where T : CodeObject
        {
            if (shouldApply)
            {
                apply(c);
            }

            return c;
        }

        public static PropertyDeclaration AddAutoGet(this PropertyDeclaration p)
        {
            return p.Apply(p => p.Getter = GetAutoAccessor());
        }

        private static PropertyAccessorDeclaration GetAutoAccessor()
        {
            return new PropertyAccessorDeclaration() { Statements = null };
        }

        public static PropertyDeclaration AddAutoSet(this PropertyDeclaration p)
        {
            return p.Apply(p => p.Setter = GetAutoAccessor());
        }
    }
}