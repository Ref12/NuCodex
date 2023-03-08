namespace Codex.ObjectModel.Attributes
{
    public enum SearchBehavior
    {
        None,
        Term,
        NormalizedKeyword,
        Sortword,
        HierarchicalPath,
        FullText,
        PrefixTerm,
        PrefixShortName,
        PrefixFullName
    }
}