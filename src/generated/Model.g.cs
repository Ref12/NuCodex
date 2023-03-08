namespace Codex.ObjectModel.Implementation
{
    public class DocumentationInfo : IDocumentationInfo
    {
        public string DeclarationName { get; set; }

        public string Comment { get; set; }

        public CodeSymbol AssociatedSymbol { get; set; }

        ICodeSymbol IDocumentationInfo.AssociatedSymbol
        {
            get
            {
                return AssociatedSymbol;
            }
        }

        public List<DocumentationReferenceSymbol> References { get; set; } = new();
        IReadOnlyList<IDocumentationReferenceSymbol> IDocumentationInfo.References
        {
            get
            {
                return References;
            }
        }

        public string Summary { get; set; }

        public string Remarks { get; set; }

        public List<TypedParameterDocumentation> Arguments { get; set; } = new();
        IReadOnlyList<ITypedParameterDocumentation> IDocumentationInfo.Arguments
        {
            get
            {
                return Arguments;
            }
        }

        public List<ParameterDocumentation> TypeParameters { get; set; } = new();
        IReadOnlyList<IParameterDocumentation> IDocumentationInfo.TypeParameters
        {
            get
            {
                return TypeParameters;
            }
        }

        public List<TypedParameterDocumentation> Exceptions { get; set; } = new();
        IReadOnlyList<ITypedParameterDocumentation> IDocumentationInfo.Exceptions
        {
            get
            {
                return Exceptions;
            }
        }
    }

    public class ParameterDocumentation : IParameterDocumentation
    {
        public string Name { get; set; }

        public string Comment { get; set; }
    }

    public class TypedParameterDocumentation : ParameterDocumentation, ITypedParameterDocumentation
    {
        public DocumentationReferenceSymbol Type { get; set; }

        IDocumentationReferenceSymbol ITypedParameterDocumentation.Type
        {
            get
            {
                return Type;
            }
        }
    }

    public class DocumentationReferenceSymbol : ReferenceSymbol, IDocumentationReferenceSymbol, ICodeSymbol
    {
        public string DisplayName { get; set; }

        public string Comment { get; set; }
    }

    public class LanguageInfo : ILanguageInfo
    {
        public string Name { get; set; }

        public List<ClassificationStyle> Classifications { get; set; } = new();
        IReadOnlyList<IClassificationStyle> ILanguageInfo.Classifications
        {
            get
            {
                return Classifications;
            }
        }
    }

    public class ClassificationStyle : IClassificationStyle
    {
        public int Color { get; set; }

        public bool Italic { get; set; }

        public string Name { get; set; }
    }

    public class SearchEntity : ISearchEntity
    {
        public string Uid { get; set; }

        public string EntityContentId { get; set; }

        public int EntityContentSize { get; set; }

        public Nullable<long> EntityVersion { get; set; }

        public int RoutingGroup { get; set; }

        public int StableId { get; set; }

        public string SortKey { get; set; }
    }

    public class RepoScopeEntity : IRepoScopeEntity
    {
        public string RepositoryName { get; set; }
    }

    public class CommitScopeEntity : RepoScopeEntity, ICommitScopeEntity
    {
        public string CommitId { get; set; }
    }

    public class ProjectScopeEntity : RepoScopeEntity, IProjectScopeEntity
    {
        public string ProjectId { get; set; }
    }

    public class RepoFileScopeEntity : RepoScopeEntity, IRepoFileScopeEntity
    {
        public string RepoRelativePath { get; set; }
    }

    public class ProjectFileScopeEntity : RepoFileScopeEntity, IProjectFileScopeEntity, IRepoScopeEntity, IProjectScopeEntity
    {
        public string ProjectRelativePath { get; set; }

        public string ProjectId { get; set; }
    }

    public class DefinitionSymbol : ReferenceSymbol, IDefinitionSymbol, ICodeSymbol
    {
        public string Uid { get; set; }

        public string DisplayName { get; set; }

        public string AbbreviatedName { get; set; }

        public List<string> Keywords { get; set; } = new();
        IReadOnlyList<string> IDefinitionSymbol.Keywords
        {
            get
            {
                return Keywords;
            }
        }

        public string ShortName { get; set; }

        public string ContainerQualifiedName { get; set; }

        public List<string> Modifiers { get; set; } = new();
        IReadOnlyList<string> IDefinitionSymbol.Modifiers
        {
            get
            {
                return Modifiers;
            }
        }

        public Glyph Glyph { get; set; }

        public int SymbolDepth { get; set; }

        public DocumentationInfo DocumentationInfo { get; set; }

        IDocumentationInfo IDefinitionSymbol.DocumentationInfo
        {
            get
            {
                return DocumentationInfo;
            }
        }

        public string TypeName { get; set; }

        public string DeclarationName { get; set; }

        public string Comment { get; set; }
    }

    public class ReferenceSymbol : CodeSymbol, IReferenceSymbol
    {
        public string ReferenceKind { get; set; }

        public bool IsImplicitlyDeclared { get; set; }

        public bool ExcludeFromDefaultSearch { get; set; }

        public bool ExcludeFromSearch { get; set; }
    }

    public class CodeSymbol : ICodeSymbol
    {
        public string ProjectId { get; set; }

        public SymbolId Id { get; set; }

        public string Kind { get; set; }
    }

    public class Commit : CommitScopeEntity, ICommit, IRepoScopeEntity
    {
        public string Description { get; set; }

        public DateTime DateUploaded { get; set; }

        public DateTime DateCommitted { get; set; }

        public List<string> ParentCommitIds { get; set; } = new();
        IReadOnlyList<string> ICommit.ParentCommitIds
        {
            get
            {
                return ParentCommitIds;
            }
        }

        public List<CommitChangedFile> ChangedFiles { get; set; } = new();
        IReadOnlyList<ICommitChangedFile> ICommit.ChangedFiles
        {
            get
            {
                return ChangedFiles;
            }
        }
    }

    public class CommitChangedFile : CommitFileLink, ICommitChangedFile
    {
        public FileChangeKind ChangeKind { get; set; }

        public string OriginalFilePath { get; set; }
    }

    public class CommitFileLink : ICommitFileLink
    {
        public string RepoRelativePath { get; set; }

        public string FileId { get; set; }

        public string VersionControlFileId { get; set; }
    }

    public class Branch : IBranch
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string HeadCommitId { get; set; }
    }

    public class BoundSourceFile : BoundSourceInfo, IBoundSourceFile
    {
        public SourceFile SourceFile { get; set; }

        ISourceFile IBoundSourceFile.SourceFile
        {
            get
            {
                return SourceFile;
            }
        }

        public Commit Commit { get; set; }

        ICommit IBoundSourceFile.Commit
        {
            get
            {
                return Commit;
            }
        }

        public Repository Repo { get; set; }

        IRepository IBoundSourceFile.Repo
        {
            get
            {
                return Repo;
            }
        }
    }

    public class BoundSourceInfo : IBoundSourceInfo
    {
        public int ReferenceCount { get; set; }

        public int DefinitionCount { get; set; }

        public string Language { get; set; }

        public List<ReferenceSpan> References { get; set; } = new();
        IReadOnlyList<IReferenceSpan> IBoundSourceInfo.References
        {
            get
            {
                return References;
            }
        }

        public List<DefinitionSpan> Definitions { get; set; } = new();
        IReadOnlyList<IDefinitionSpan> IBoundSourceInfo.Definitions
        {
            get
            {
                return Definitions;
            }
        }

        public List<ClassificationSpan> Classifications { get; set; } = new();
        IReadOnlyList<IClassificationSpan> IBoundSourceInfo.Classifications
        {
            get
            {
                return Classifications;
            }
        }

        public List<OutliningRegion> OutliningRegions { get; set; } = new();
        IReadOnlyList<IOutliningRegion> IBoundSourceInfo.OutliningRegions
        {
            get
            {
                return OutliningRegions;
            }
        }

        public bool ExcludeFromSearch { get; set; }
    }

    public class SourceControlFileInfo : ISourceControlFileInfo
    {
        public string SourceControlContentId { get; set; }
    }

    public class SourceFileInfo : RepoFileScopeEntity, ISourceFileInfo, IRepoScopeEntity, ISourceControlFileInfo, IProjectFileScopeEntity, IProjectScopeEntity
    {
        public int Lines { get; set; }

        public int Size { get; set; }

        public string Language { get; set; }

        public string WebAddress { get; set; }

        public EncodingDescription Encoding { get; set; }

        IEncodingDescription ISourceFileInfo.Encoding
        {
            get
            {
                return Encoding;
            }
        }

        public PropertyMap Properties { get; set; }

        IPropertyMap ISourceFileInfo.Properties
        {
            get
            {
                return Properties;
            }
        }

        public string SourceControlContentId { get; set; }

        public string ProjectRelativePath { get; set; }

        public string ProjectId { get; set; }
    }

    public class EncodingDescription : IEncodingDescription
    {
        public string Name { get; set; }

        public byte[] Preamble { get; set; }
    }

    public class SourceFileBase : ISourceFileBase
    {
        public SourceFileInfo Info { get; set; }

        ISourceFileInfo ISourceFileBase.Info
        {
            get
            {
                return Info;
            }
        }

        public bool ExcludeFromSearch { get; set; }
    }

    public class SourceFile : SourceFileBase, ISourceFile
    {
        public string Content { get; set; }
    }

    public class ChunkedSourceFile : SourceFileBase, IChunkedSourceFile
    {
        public List<ChunkReference> Chunks { get; set; } = new();
        IReadOnlyList<IChunkReference> IChunkedSourceFile.Chunks
        {
            get
            {
                return Chunks;
            }
        }
    }

    public class ChunkReference : IChunkReference
    {
        public string Id { get; set; }

        public int StartLineNumber { get; set; }
    }

    public class SourceFileContentChunk : ISourceFileContentChunk
    {
        public List<string> ContentLines { get; set; } = new();
        IReadOnlyList<string> ISourceFileContentChunk.ContentLines
        {
            get
            {
                return ContentLines;
            }
        }
    }

    public class OutliningRegion : IOutliningRegion
    {
        public string Kind { get; set; }

        public LineSpan Header { get; set; }

        ILineSpan IOutliningRegion.Header
        {
            get
            {
                return Header;
            }
        }

        public LineSpan Content { get; set; }

        ILineSpan IOutliningRegion.Content
        {
            get
            {
                return Content;
            }
        }
    }

    public class DefinitionSpan : Span, IDefinitionSpan
    {
        public DefinitionSymbol Definition { get; set; }

        IDefinitionSymbol IDefinitionSpan.Definition
        {
            get
            {
                return Definition;
            }
        }

        public List<ParameterDefinitionSpan> Parameters { get; set; } = new();
        IReadOnlyList<IParameterDefinitionSpan> IDefinitionSpan.Parameters
        {
            get
            {
                return Parameters;
            }
        }
    }

    public class ParameterDefinitionSpan : LineSpan, IParameterDefinitionSpan, ISpan
    {
        public int ParameterIndex { get; set; }

        public string Name { get; set; }
    }

    public class ReferenceSpan : SymbolSpan, IReferenceSpan, ITextLineSpan, ILineSpan, ISpan
    {
        public SymbolId RelatedDefinition { get; set; }

        public ReferenceSymbol Reference { get; set; }

        IReferenceSymbol IReferenceSpan.Reference
        {
            get
            {
                return Reference;
            }
        }

        public List<ParameterReferenceSpan> Parameters { get; set; } = new();
        IReadOnlyList<IParameterReferenceSpan> IReferenceSpan.Parameters
        {
            get
            {
                return Parameters;
            }
        }
    }

    public class ParameterReferenceSpan : SymbolSpan, IParameterReferenceSpan, ITextLineSpan, ILineSpan, ISpan
    {
        public int ParameterIndex { get; set; }
    }

    public class ClassificationSpan : Span, IClassificationSpan
    {
        public int DefaultClassificationColor { get; set; }

        public string Classification { get; set; }

        public int LocalGroupId { get; set; }
    }

    public class SymbolSpan : TextLineSpan, ISymbolSpan, ILineSpan, ISpan
    {
    }

    public class TextLineSpan : LineSpan, ITextLineSpan, ISpan
    {
        public string LineSpanText { get; set; }
    }

    public class LineSpan : Span, ILineSpan
    {
        public int LineIndex { get; set; }

        public int LineNumber { get; set; }

        public int LineSpanStart { get; set; }

        public int LineOffset { get; set; }
    }

    public class Span : ISpan
    {
        public int Start { get; set; }

        public int Length { get; set; }
    }

    public class CodeReview : ICodeReview
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public CodeReviewStatus Status { get; set; }
    }

    public class CodeReviewIteration : ICodeReviewIteration
    {
        public int IterationNumber { get; set; }

        public string ReviewId { get; set; }

        public string Description { get; set; }

        public List<CodeReviewFile> Files { get; set; } = new();
        IReadOnlyList<ICodeReviewFile> ICodeReviewIteration.Files
        {
            get
            {
                return Files;
            }
        }
    }

    public class CodeReviewerInfo : ICodeReviewerInfo
    {
        public string Name { get; set; }
    }

    public class CodeReviewFile : ICodeReviewFile
    {
        public int StartIteration { get; set; }

        public string RepoRelativePath { get; set; }

        public string FileId { get; set; }

        public string BaselineFileId { get; set; }

        public FileChangeKind ChangeKind { get; set; }
    }

    public class CodeReviewCommentThread : ICodeReviewCommentThread
    {
        public LineSpan OriginalSpan { get; set; }

        ILineSpan ICodeReviewCommentThread.OriginalSpan
        {
            get
            {
                return OriginalSpan;
            }
        }

        public int StartIteration { get; set; }

        public DateTime LastUpdated { get; set; }

        public string FileRepoRelativePath { get; set; }

        public List<CodeReviewComment> Comments { get; set; } = new();
        IReadOnlyList<ICodeReviewComment> ICodeReviewCommentThread.Comments
        {
            get
            {
                return Comments;
            }
        }
    }

    public class CodeReviewComment : ICodeReviewComment
    {
        public string Text { get; set; }

        public string Reviewer { get; set; }

        public CommentImportance Importance { get; set; }

        public DateTime CommentTime { get; set; }
    }

    public class AnalyzedProject : ProjectScopeEntity, IAnalyzedProject, IRepoScopeEntity
    {
        public string ProjectKind { get; set; }

        public ProjectFileLink PrimaryFile { get; set; }

        IProjectFileLink IAnalyzedProject.PrimaryFile
        {
            get
            {
                return PrimaryFile;
            }
        }

        public List<ProjectFileLink> Files { get; set; } = new();
        IReadOnlyList<IProjectFileLink> IAnalyzedProject.Files
        {
            get
            {
                return Files;
            }
        }

        public List<ReferencedProject> ProjectReferences { get; set; } = new();
        IReadOnlyList<IReferencedProject> IAnalyzedProject.ProjectReferences
        {
            get
            {
                return ProjectReferences;
            }
        }
    }

    public class ReferencedProject : IReferencedProject
    {
        public string ProjectId { get; set; }

        public List<DefinitionSymbol> Definitions { get; set; } = new();
        IReadOnlyList<IDefinitionSymbol> IReferencedProject.Definitions
        {
            get
            {
                return Definitions;
            }
        }

        public string DisplayName { get; set; }

        public PropertyMap Properties { get; set; }

        IPropertyMap IReferencedProject.Properties
        {
            get
            {
                return Properties;
            }
        }
    }

    public class ProjectFileLink : ProjectFileScopeEntity, IProjectFileLink, IRepoFileScopeEntity, IRepoScopeEntity, IProjectScopeEntity
    {
        public string FileId { get; set; }
    }

    public class PropertySearchModel : SearchEntity, IPropertySearchModel, ISearchEntityBase
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public string OwnerId { get; set; }
    }

    public class PropertyMap : IPropertyMap
    {
    }

    public class Repository : IRepository
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string SourceControlWebAddress { get; set; }

        public string FileWebAddressTransformInputRegex { get; set; }

        public string PrimaryBranch { get; set; }

        public List<RepositoryReference> RepositoryReferences { get; set; } = new();
        IReadOnlyList<IRepositoryReference> IRepository.RepositoryReferences
        {
            get
            {
                return RepositoryReferences;
            }
        }
    }

    public class RepositoryReference : IRepositoryReference
    {
        public string Name { get; set; }

        public string Id { get; set; }
    }
}