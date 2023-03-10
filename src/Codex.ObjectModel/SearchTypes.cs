using Codex.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Codex.ObjectModel
{
    /*
     * Types in this file define search behaviors. Changes should be made with caution as they can affect
     * the mapping schema for indices and will generally need to be backward compatible.
     * Additions should be generally safe.
     * 
     * WARNING: Changing routing key by changed input to Route() function is extremely destructive as it causes entities
     * to be routed to different shards and thereby invalidates most stored documents. Generally, these should never be changed
     * unless the entire index will be recreated.
     * 
     * TODO: Maybe there should be some sort of validation on this.
     */
    public class SearchTypes
    {
        public static readonly List<SearchType> RegisteredSearchTypes = new List<SearchType>();

        public static SearchType<IDefinitionSearchModel> Definition = SearchType.Create<IDefinitionSearchModel>(RegisteredSearchTypes)
            .Route(ds => ds.Definition.Id.Value);
        //.CopyTo(ds => ds.Definition.Modifiers, ds => ds.Keywords)
        //.CopyTo(ds => ds.Definition.Kind, ds => ds.Kind)
        //.CopyTo(ds => ds.Definition.ExcludeFromDefaultSearch, ds => ds.ExcludeFromDefaultSearch)
        //.CopyTo(ds => ds.Definition.Kind, ds => ds.Keywords)
        //.CopyTo(ds => ds.Definition.ShortName, ds => ds.ShortName)
        ////.CopyTo(ds => ds.Language, ds => ds.Keywords)
        //.CopyTo(ds => ds.Definition.ProjectId, ds => ds.ProjectId)
        //.CopyTo(ds => ds.Definition.ProjectId, ds => ds.Keywords);

        public static SearchType<IReferenceSearchModel> Reference = SearchType.Create<IReferenceSearchModel>(RegisteredSearchTypes)
            .Route(rs => rs.Reference.Id.Value);
        //.CopyTo(rs => rs.Spans.First().Reference, rs => rs.Reference);


        public static SearchType<ITextChunkSearchModel> TextChunk = SearchType.Create<ITextChunkSearchModel>(RegisteredSearchTypes);

        public static SearchType<ITextSourceSearchModel> TextSource = SearchType.Create<ITextSourceSearchModel>(RegisteredSearchTypes)
            .Route(ss => Path.GetFileName(ss.File.Info.RepoRelativePath));
        //.CopyTo(ss => ss.File.SourceFile.Content, ss => ss.Content)
        //.CopyTo(ss => ss.File.SourceFile.Info.RepoRelativePath, ss => ss.RepoRelativePath)
        //.CopyTo(ss => ss.File.ProjectId, ss => ss.ProjectId)
        //.CopyTo(ss => ss.File.Info.Path, ss => ss.FilePath);

        public static SearchType<IBoundSourceSearchModel> BoundSource = SearchType.Create<IBoundSourceSearchModel>(RegisteredSearchTypes)
            .Route(ss => Path.GetFileName(ss.File.Info.RepoRelativePath));
        //.CopyTo(ss => ss.File.SourceFile.Content, ss => ss.Content)
        //.CopyTo(ss => ss.File.SourceFile.Info.RepoRelativePath, ss => ss.RepoRelativePath)
        //.CopyTo(ss => ss.BindingInfo.ProjectId, ss => ss.ProjectId)
        //.CopyTo(ss => ss.FilePath, ss => ss.FilePath);

        public static SearchType<ILanguageSearchModel> Language = SearchType.Create<ILanguageSearchModel>(RegisteredSearchTypes)
            .Route(ls => ls.Language.Name);

        public static SearchType<IRepositorySearchModel> Repository = SearchType.Create<IRepositorySearchModel>(RegisteredSearchTypes)
            .Route(rs => rs.Repository.Name);

        public static SearchType<IProjectSearchModel> Project = SearchType.Create<IProjectSearchModel>(RegisteredSearchTypes)
            .Route(sm => sm.Project.ProjectId)
            .Exclude(sm => sm.Project.ProjectReferences.First().Definitions);

        public static SearchType<ICommitSearchModel> Commit = SearchType.Create<ICommitSearchModel>(RegisteredSearchTypes);

        // TODO: Should these be one per file to allow mapping text files to their corresponding project for text search
        public static SearchType<ICommitFilesSearchModel> CommitFiles = SearchType.Create<ICommitFilesSearchModel>(RegisteredSearchTypes);

        public static SearchType<IProjectReferenceSearchModel> ProjectReference = SearchType.Create<IProjectReferenceSearchModel>(RegisteredSearchTypes);

        public static SearchType<IPropertySearchModel> Property = SearchType.Create<IPropertySearchModel>(RegisteredSearchTypes);

        public static SearchType<IStoredFilter> StoredFilter = SearchType.Create<IStoredFilter>(RegisteredSearchTypes);

        public static SearchType<IRegisteredEntity> RegisteredEntity = SearchType.Create<IRegisteredEntity>(RegisteredSearchTypes);
    }

    /// <summary>
    /// In order to compute a stable integral id for each entity. This type is used to store into a 'follow' index which
    /// stores entities of this type using the <see cref="ISearchEntity.Uid"/> of the corresponding search entity. Then the
    /// sequence number assigned by ElasticSearch is used as the shard stable id (<see cref="ISearchEntity.StableId"/>)
    /// for the entity. This approach is used in order to ensure that the stable id appears as an explicit field in the document rather
    /// which allows configuration of how the field is indexed (not true for sequence number field without code changes to ES).
    /// </summary>
    public interface IRegisteredEntity : ISearchEntity
    {
        /// <summary>
        /// The date in which the entity was registered (i.e. added to the store)
        /// </summary>
        DateTime DateAdded { get; set; }

        /// <summary>
        /// The id of the originating entity
        /// </summary>
        [SearchBehavior(SearchBehavior.Term)]
        string EntityUid { get; set; }

        /// <summary>
        /// The index of the originating entity
        /// </summary>
        [SearchBehavior(SearchBehavior.Term)]
        string IndexName { get; set; }
    }

    /// <summary>
    /// Defines a stored filter which matches entities in a particular index shard in a stable manner
    /// </summary>
    public interface IStoredFilter : ISearchEntity
    {
        /// <summary>
        /// The time of the last update to the stored filter
        /// </summary>
        DateTime DateUpdated { get; set; }

        string FullPath { get; set; }

        /// <summary>
        /// The name of the stored filter
        /// </summary>
        [SearchBehavior(SearchBehavior.Term)]
        string Name { get; set; }

        /// <summary>
        /// The name of the index to which the stored filter applies
        /// </summary>
        [SearchBehavior(SearchBehavior.Term)]
        string IndexName { get; }

        /// <summary>
        /// Stored filter bit set
        /// </summary>
        [SearchBehavior(SearchBehavior.None)]
        byte[] StableIds { get; }

        [SearchBehavior(SearchBehavior.None)]
        IReadOnlyList<IChildFilterReference> Children { get; }

        /// <summary>
        /// The hash of <see cref="Filter"/>
        /// </summary>
        [SearchBehavior(SearchBehavior.Term)]
        string FilterHash { get; }

        /// <summary>
        /// The count of elements matched by <see cref="Filter"/>
        /// </summary>
        int Cardinality { get; }
    }

    public interface IChildFilterReference
    {
        string FullPath { get; }

        /// <summary>
        /// The <see cref="ISearchEntity.Uid"/> of the child filter
        /// </summary>
        string Uid { get; }

        /// <summary>
        /// The <see cref="IStoredFilter.StableIds"/> of the child filter
        /// </summary>
        byte[] StableIds { get; }

        /// <summary>
        /// The <see cref="IStoredFilter.Cardinality"/> of the child filter
        /// </summary>
        int Cardinality { get; }
    }

    [AdapterType]
    public interface IGroupedStoredFilterIds
    {
    }

    public interface IDefinitionSearchModel : ISearchEntity
    {
        IDefinitionSymbol Definition { get; }

        // TODO: Not sure that this is actually needed
        /// <summary>
        /// Keywords are additional terms which can be used to find a given symbol.
        /// NOTE: Keywords can only be used to select from symbols which have
        /// a primary term match
        /// </summary>
        [SearchBehavior(SearchBehavior.NormalizedKeyword)]
        IReadOnlyList<string> Keywords { get; }
    }

    public interface ILanguageSearchModel : ISearchEntity
    {
        ILanguageInfo Language { get; }
    }

    public interface IReferenceSearchModel : IProjectFileScopeEntity, ISearchEntity
    {
        /// <summary>
        /// The reference symbol
        /// </summary>
        IReferenceSymbol Reference { get; }

        [Placeholder.Todo("Need some sort of override for searching RelatedDefinition of the ReferenceSpan")]
        [SearchBehavior(SearchBehavior.None)]
        [ReadOnlyList]
        [CoerceGet]
        IReadOnlyList<ISymbolSpan> Spans { get; }

        /// <summary>
        /// Compressed list of spans
        /// </summary>
        [SearchBehavior(SearchBehavior.None)]
        ISymbolLineSpanListModel CompressedSpans { get; }
    }

    public interface ISourceSearchModelBase : ISearchEntity
    {
        /// <summary>
        /// Information about the source file from source control provider (may be null)
        /// </summary>
        ISourceControlFileInfo SourceControlInfo { get; }

        IChunkedSourceFile File { get; }
    }

    public interface IBoundSourceSearchModel : ISourceSearchModelBase
    {
        /// <summary>
        /// The unique identifier of the associated <see cref="ISourceFile"/>
        /// </summary>
        string TextUid { get; }

        /// <summary>
        /// The binding info
        /// </summary>
        IBoundSourceInfo BindingInfo { get; }

        /// <summary>
        /// Compressed list of classification spans
        /// </summary>
        [SearchBehavior(SearchBehavior.None)]
        IClassificationListModel CompressedClassifications { get; }

        /// <summary>
        /// Compressed list of reference spans
        /// </summary>
        [SearchBehavior(SearchBehavior.None)]
        IReferenceListModel CompressedReferences { get; }
    }

    public interface ITextSourceSearchModel : ISourceSearchModelBase
    {
    }

    public interface ITextChunkSearchModel : ISearchEntity
    {
        /// <summary>
        /// The text content. Set when the chunk IS searched
        /// </summary>
        ISourceFileContentChunk Chunk { get; }

        /// <summary>
        /// The text content. Set when the chunk IS NOT searched
        /// </summary>
        [SearchBehavior(SearchBehavior.None)]
        ISourceFileContentChunk RawChunk { get; }
    }

    public interface IRepositorySearchModel : ISearchEntity
    {
        IRepository Repository { get; }
    }

    public interface IProjectSearchModel : ISearchEntity
    {
        IAnalyzedProject Project { get; }
    }

    public interface IProjectReferenceSearchModel : IProjectScopeEntity, ISearchEntity
    {
        IReferencedProject ProjectReference { get; }
    }

    public interface ICommitSearchModel : ISearchEntity
    {
        ICommit Commit { get; }
    }

    /// <summary>
    /// The set of files present in the repository at a given commit.
    /// </summary>
    public interface ICommitFilesSearchModel : ICommitScopeEntity, IRepoScopeEntity, ISearchEntity
    {
        IReadOnlyList<ICommitFileLink> CommitFiles { get; }
    }
}

