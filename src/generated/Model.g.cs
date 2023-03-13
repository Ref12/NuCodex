namespace Codex.ObjectModel.Implementation
{
    using static PropertyTarget;

    public partial class DocumentationInfo : EntityBase, IDocumentationInfo, IPropertyTarget<IDocumentationInfo>
    {
        public DocumentationInfo()
        {
        }

        public DocumentationInfo(IDocumentationInfo source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new DocumentationInfo(((IDocumentationInfo)this));
        }

        public void CopyFrom(IDocumentationInfo source)
        {
            DeclarationName = GetOrCopy<string, string>(DeclarationName, source.DeclarationName);
            Comment = GetOrCopy<string, string>(Comment, source.Comment);
            AssociatedSymbol = GetOrCopy<CodeSymbol, ICodeSymbol>(AssociatedSymbol, source.AssociatedSymbol);
            References = GetOrCopy<DocumentationReferenceSymbol, IDocumentationReferenceSymbol>(References, source.References);
            Summary = GetOrCopy<string, string>(Summary, source.Summary);
            Remarks = GetOrCopy<string, string>(Remarks, source.Remarks);
            Arguments = GetOrCopy<TypedParameterDocumentation, ITypedParameterDocumentation>(Arguments, source.Arguments);
            TypeParameters = GetOrCopy<ParameterDocumentation, IParameterDocumentation>(TypeParameters, source.TypeParameters);
            Exceptions = GetOrCopy<TypedParameterDocumentation, ITypedParameterDocumentation>(Exceptions, source.Exceptions);
        }

        IReadOnlyList<IParameterDocumentation> IDocumentationInfo.TypeParameters
        {
            get
            {
                return TypeParameters;
            }
        }

        public List<ParameterDocumentation> TypeParameters { get; set; } = new();
        IReadOnlyList<ITypedParameterDocumentation> IDocumentationInfo.Arguments
        {
            get
            {
                return Arguments;
            }
        }

        public List<TypedParameterDocumentation> Arguments { get; set; } = new();
        public string Remarks { get; set; }

        public string Summary { get; set; }

        public List<DocumentationReferenceSymbol> References { get; set; } = new();
        public List<TypedParameterDocumentation> Exceptions { get; set; } = new();
        ICodeSymbol IDocumentationInfo.AssociatedSymbol
        {
            get
            {
                return AssociatedSymbol;
            }
        }

        public CodeSymbol AssociatedSymbol { get; set; }

        public string Comment { get; set; }

        public string DeclarationName { get; set; }

        IReadOnlyList<IDocumentationReferenceSymbol> IDocumentationInfo.References
        {
            get
            {
                return References;
            }
        }

        IReadOnlyList<ITypedParameterDocumentation> IDocumentationInfo.Exceptions
        {
            get
            {
                return Exceptions;
            }
        }
    }

    public partial class ParameterDocumentation : EntityBase, IParameterDocumentation, IPropertyTarget<IParameterDocumentation>
    {
        public ParameterDocumentation()
        {
        }

        public ParameterDocumentation(IParameterDocumentation source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new ParameterDocumentation(((IParameterDocumentation)this));
        }

        public void CopyFrom(IParameterDocumentation source)
        {
            Name = GetOrCopy<string, string>(Name, source.Name);
            Comment = GetOrCopy<string, string>(Comment, source.Comment);
        }

        public string Name { get; set; }

        public string Comment { get; set; }
    }

    public partial class TypedParameterDocumentation : ParameterDocumentation, ITypedParameterDocumentation, IPropertyTarget<ITypedParameterDocumentation>
    {
        public TypedParameterDocumentation()
        {
        }

        public TypedParameterDocumentation(ITypedParameterDocumentation source)
        {
            this.CopyFrom(source);
        }

        public TypedParameterDocumentation(IParameterDocumentation source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new TypedParameterDocumentation(((ITypedParameterDocumentation)this));
        }

        public void CopyFrom(ITypedParameterDocumentation source)
        {
            Type = GetOrCopy<DocumentationReferenceSymbol, IDocumentationReferenceSymbol>(Type, source.Type);
        }

        public DocumentationReferenceSymbol Type { get; set; }

        IDocumentationReferenceSymbol ITypedParameterDocumentation.Type
        {
            get
            {
                return Type;
            }
        }
    }

    public partial class DocumentationReferenceSymbol : ReferenceSymbol, IDocumentationReferenceSymbol, ICodeSymbol, IPropertyTarget<IDocumentationReferenceSymbol>
    {
        public DocumentationReferenceSymbol()
        {
        }

        public DocumentationReferenceSymbol(IReferenceSymbol source)
        {
            this.CopyFrom(source);
        }

        public DocumentationReferenceSymbol(IDocumentationReferenceSymbol source)
        {
            this.CopyFrom(source);
        }

        public DocumentationReferenceSymbol(ICodeSymbol source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new DocumentationReferenceSymbol(((IDocumentationReferenceSymbol)this));
        }

        public void CopyFrom(IDocumentationReferenceSymbol source)
        {
            DisplayName = GetOrCopy<string, string>(DisplayName, source.DisplayName);
            Comment = GetOrCopy<string, string>(Comment, source.Comment);
        }

        public string DisplayName { get; set; }

        public string Comment { get; set; }
    }

    public partial class LanguageInfo : EntityBase, ILanguageInfo, IPropertyTarget<ILanguageInfo>
    {
        public LanguageInfo()
        {
        }

        public LanguageInfo(ILanguageInfo source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new LanguageInfo(((ILanguageInfo)this));
        }

        public void CopyFrom(ILanguageInfo source)
        {
            Name = GetOrCopy<string, string>(Name, source.Name);
            Classifications = GetOrCopy<ClassificationStyle, IClassificationStyle>(Classifications, source.Classifications);
        }

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

    public partial class ClassificationStyle : EntityBase, IClassificationStyle, IPropertyTarget<IClassificationStyle>
    {
        public ClassificationStyle()
        {
        }

        public ClassificationStyle(IClassificationStyle source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new ClassificationStyle(((IClassificationStyle)this));
        }

        public void CopyFrom(IClassificationStyle source)
        {
            Color = GetOrCopy<int, int>(Color, source.Color);
            Italic = GetOrCopy<bool, bool>(Italic, source.Italic);
            Name = GetOrCopy<string, string>(Name, source.Name);
        }

        public int Color { get; set; }

        public bool Italic { get; set; }

        public string Name { get; set; }
    }

    public partial class SymbolLineSpanList : EntityBase, ISymbolLineSpanList, IPropertyTarget<ISymbolLineSpanList>
    {
        public SymbolLineSpanList()
        {
        }

        public SymbolLineSpanList(ISymbolLineSpanList source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new SymbolLineSpanList(((ISymbolLineSpanList)this));
        }

        public void CopyFrom(ISymbolLineSpanList source)
        {
        }
    }

    public partial class ClassificationList : EntityBase, IClassificationList, IPropertyTarget<IClassificationList>
    {
        public ClassificationList()
        {
        }

        public ClassificationList(IClassificationList source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new ClassificationList(((IClassificationList)this));
        }

        public void CopyFrom(IClassificationList source)
        {
        }
    }

    public partial class ReferenceList : EntityBase, IReferenceList, IPropertyTarget<IReferenceList>
    {
        public ReferenceList()
        {
        }

        public ReferenceList(IReferenceList source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new ReferenceList(((IReferenceList)this));
        }

        public void CopyFrom(IReferenceList source)
        {
        }
    }

    public partial class SearchEntityBase : EntityBase, ISearchEntityBase, IPropertyTarget<ISearchEntityBase>
    {
        public SearchEntityBase()
        {
        }

        public SearchEntityBase(ISearchEntityBase source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new SearchEntityBase(((ISearchEntityBase)this));
        }

        public void CopyFrom(ISearchEntityBase source)
        {
        }
    }

    public partial class SearchEntity : SearchEntityBase, ISearchEntity, IPropertyTarget<ISearchEntity>
    {
        public SearchEntity()
        {
        }

        public SearchEntity(ISearchEntity source)
        {
            this.CopyFrom(source);
        }

        public SearchEntity(ISearchEntityBase source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new SearchEntity(((ISearchEntity)this));
        }

        public void CopyFrom(ISearchEntity source)
        {
            Uid = GetOrCopy<string, string>(Uid, source.Uid);
            EntityContentId = GetOrCopy<string, string>(EntityContentId, source.EntityContentId);
            EntityContentSize = GetOrCopy<int, int>(EntityContentSize, source.EntityContentSize);
            EntityVersion = GetOrCopy<Nullable<long>, Nullable<long>>(EntityVersion, source.EntityVersion);
            RoutingGroup = GetOrCopy<int, int>(RoutingGroup, source.RoutingGroup);
            StableId = GetOrCopy<int, int>(StableId, source.StableId);
            SortKey = GetOrCopy<string, string>(SortKey, source.SortKey);
        }

        public string Uid { get; set; }

        public string EntityContentId { get; set; }

        public int EntityContentSize { get; set; }

        public Nullable<long> EntityVersion { get; set; }

        public int RoutingGroup { get; set; }

        public int StableId { get; set; }

        public string SortKey { get; set; }
    }

    public partial class RepoScopeEntity : EntityBase, IRepoScopeEntity, IPropertyTarget<IRepoScopeEntity>
    {
        public RepoScopeEntity()
        {
        }

        public RepoScopeEntity(IRepoScopeEntity source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new RepoScopeEntity(((IRepoScopeEntity)this));
        }

        public void CopyFrom(IRepoScopeEntity source)
        {
            RepositoryName = GetOrCopy<string, string>(RepositoryName, source.RepositoryName);
        }

        public string RepositoryName { get; set; }
    }

    public partial class CommitScopeEntity : RepoScopeEntity, ICommitScopeEntity, IPropertyTarget<ICommitScopeEntity>
    {
        public CommitScopeEntity()
        {
        }

        public CommitScopeEntity(IRepoScopeEntity source)
        {
            this.CopyFrom(source);
        }

        public CommitScopeEntity(ICommitScopeEntity source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new CommitScopeEntity(((ICommitScopeEntity)this));
        }

        public void CopyFrom(ICommitScopeEntity source)
        {
            CommitId = GetOrCopy<string, string>(CommitId, source.CommitId);
        }

        public string CommitId { get; set; }
    }

    public partial class ProjectScopeEntity : RepoScopeEntity, IProjectScopeEntity, IPropertyTarget<IProjectScopeEntity>
    {
        public ProjectScopeEntity()
        {
        }

        public ProjectScopeEntity(IRepoScopeEntity source)
        {
            this.CopyFrom(source);
        }

        public ProjectScopeEntity(IProjectScopeEntity source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new ProjectScopeEntity(((IProjectScopeEntity)this));
        }

        public void CopyFrom(IProjectScopeEntity source)
        {
            ProjectId = GetOrCopy<string, string>(ProjectId, source.ProjectId);
        }

        public string ProjectId { get; set; }
    }

    public partial class RepoFileScopeEntity : RepoScopeEntity, IRepoFileScopeEntity, IPropertyTarget<IRepoFileScopeEntity>
    {
        public RepoFileScopeEntity()
        {
        }

        public RepoFileScopeEntity(IRepoScopeEntity source)
        {
            this.CopyFrom(source);
        }

        public RepoFileScopeEntity(IRepoFileScopeEntity source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new RepoFileScopeEntity(((IRepoFileScopeEntity)this));
        }

        public void CopyFrom(IRepoFileScopeEntity source)
        {
            RepoRelativePath = GetOrCopy<string, string>(RepoRelativePath, source.RepoRelativePath);
        }

        public string RepoRelativePath { get; set; }
    }

    public partial class ProjectFileScopeEntity : RepoFileScopeEntity, IProjectFileScopeEntity, IRepoScopeEntity, IProjectScopeEntity, IPropertyTarget<IProjectScopeEntity>, IPropertyTarget<IProjectFileScopeEntity>
    {
        public ProjectFileScopeEntity()
        {
        }

        public ProjectFileScopeEntity(IRepoScopeEntity source)
        {
            this.CopyFrom(source);
        }

        public ProjectFileScopeEntity(IProjectScopeEntity source)
        {
            this.CopyFrom(source);
        }

        public ProjectFileScopeEntity(IRepoFileScopeEntity source)
        {
            this.CopyFrom(source);
        }

        public ProjectFileScopeEntity(IProjectFileScopeEntity source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new ProjectFileScopeEntity(((IProjectFileScopeEntity)this));
        }

        public void CopyFrom(IProjectScopeEntity source)
        {
            ProjectId = GetOrCopy<string, string>(ProjectId, source.ProjectId);
        }

        public void CopyFrom(IProjectFileScopeEntity source)
        {
            ProjectRelativePath = GetOrCopy<string, string>(ProjectRelativePath, source.ProjectRelativePath);
        }

        public string ProjectId { get; set; }

        public string ProjectRelativePath { get; set; }
    }

    public partial class DefinitionSymbol : ReferenceSymbol, IDefinitionSymbol, ICodeSymbol, IPropertyTarget<IDefinitionSymbol>
    {
        public DefinitionSymbol()
        {
        }

        public DefinitionSymbol(IReferenceSymbol source)
        {
            this.CopyFrom(source);
        }

        public DefinitionSymbol(ICodeSymbol source)
        {
            this.CopyFrom(source);
        }

        public DefinitionSymbol(IDefinitionSymbol source)
        {
            this.CopyFrom(source);
        }

        public void CopyFrom(IDefinitionSymbol source)
        {
            Uid = GetOrCopy<string, string>(Uid, source.Uid);
            DisplayName = GetOrCopy<string, string>(DisplayName, source.DisplayName);
            AbbreviatedName = GetOrCopy<string, string>(AbbreviatedName, source.AbbreviatedName);
            Keywords = GetOrCopy<string, string>(Keywords, source.Keywords);
            ShortName = GetOrCopy<string, string>(ShortName, source.ShortName);
            ContainerQualifiedName = GetOrCopy<string, string>(ContainerQualifiedName, source.ContainerQualifiedName);
            Modifiers = GetOrCopy<string, string>(Modifiers, source.Modifiers);
            Glyph = GetOrCopy<Glyph, Glyph>(Glyph, source.Glyph);
            SymbolDepth = GetOrCopy<int, int>(SymbolDepth, source.SymbolDepth);
            DocumentationInfo = GetOrCopy<DocumentationInfo, IDocumentationInfo>(DocumentationInfo, source.DocumentationInfo);
            TypeName = GetOrCopy<string, string>(TypeName, source.TypeName);
            DeclarationName = GetOrCopy<string, string>(DeclarationName, source.DeclarationName);
            Comment = GetOrCopy<string, string>(Comment, source.Comment);
        }

        object IPropertyTarget.CreateClone()
        {
            return new DefinitionSymbol(((IDefinitionSymbol)this));
        }

        public string Comment { get; set; }

        public string DeclarationName { get; set; }

        public string TypeName { get; set; }

        IDocumentationInfo IDefinitionSymbol.DocumentationInfo
        {
            get
            {
                return DocumentationInfo;
            }
        }

        public DocumentationInfo DocumentationInfo { get; set; }

        public int SymbolDepth { get; set; }

        public Glyph Glyph { get; set; }

        IReadOnlyList<string> IDefinitionSymbol.Modifiers
        {
            get
            {
                return Modifiers;
            }
        }

        public List<string> Modifiers { get; set; } = new();
        public string ShortName
        {
            get
            {
                return CoerceShortName(this.m_ShortName);
            }

            set
            {
                this.m_ShortName = value;
            }
        }

        IReadOnlyList<string> IDefinitionSymbol.Keywords
        {
            get
            {
                return Keywords;
            }
        }

        public List<string> Keywords { get; set; } = new();
        private string m_AbbreviatedName;
        public string AbbreviatedName
        {
            get
            {
                return CoerceAbbreviatedName(this.m_AbbreviatedName);
            }

            set
            {
                this.m_AbbreviatedName = value;
            }
        }

        public string DisplayName { get; set; }

        public string Uid { get; set; }

        public string ContainerQualifiedName { get; set; }

        private string m_ShortName;
    }

    public partial class ReferenceSymbol : CodeSymbol, IReferenceSymbol, IPropertyTarget<IReferenceSymbol>
    {
        public ReferenceSymbol()
        {
        }

        public ReferenceSymbol(IReferenceSymbol source)
        {
            this.CopyFrom(source);
        }

        public ReferenceSymbol(ICodeSymbol source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new ReferenceSymbol(((IReferenceSymbol)this));
        }

        public void CopyFrom(IReferenceSymbol source)
        {
            ReferenceKind = GetOrCopy<string, string>(ReferenceKind, source.ReferenceKind);
            IsImplicitlyDeclared = GetOrCopy<bool, bool>(IsImplicitlyDeclared, source.IsImplicitlyDeclared);
            ExcludeFromDefaultSearch = GetOrCopy<bool, bool>(ExcludeFromDefaultSearch, source.ExcludeFromDefaultSearch);
            ExcludeFromSearch = GetOrCopy<bool, bool>(ExcludeFromSearch, source.ExcludeFromSearch);
        }

        public string ReferenceKind { get; set; }

        public bool IsImplicitlyDeclared { get; set; }

        public bool ExcludeFromDefaultSearch { get; set; }

        public bool ExcludeFromSearch { get; set; }
    }

    public partial class CodeSymbol : EntityBase, ICodeSymbol, IPropertyTarget<ICodeSymbol>
    {
        public CodeSymbol()
        {
        }

        public CodeSymbol(ICodeSymbol source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new CodeSymbol(((ICodeSymbol)this));
        }

        public void CopyFrom(ICodeSymbol source)
        {
            ProjectId = GetOrCopy<string, string>(ProjectId, source.ProjectId);
            Id = GetOrCopy<SymbolId, SymbolId>(Id, source.Id);
            Kind = GetOrCopy<string, string>(Kind, source.Kind);
        }

        public string ProjectId { get; set; }

        public SymbolId Id { get; set; }

        public string Kind { get; set; }
    }

    public partial class Commit : CommitScopeEntity, ICommit, IRepoScopeEntity, IPropertyTarget<ICommit>
    {
        public Commit()
        {
        }

        public Commit(IRepoScopeEntity source)
        {
            this.CopyFrom(source);
        }

        public Commit(ICommitScopeEntity source)
        {
            this.CopyFrom(source);
        }

        public Commit(ICommit source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new Commit(((ICommit)this));
        }

        public void CopyFrom(ICommit source)
        {
            Description = GetOrCopy<string, string>(Description, source.Description);
            DateUploaded = GetOrCopy<DateTime, DateTime>(DateUploaded, source.DateUploaded);
            DateCommitted = GetOrCopy<DateTime, DateTime>(DateCommitted, source.DateCommitted);
            ParentCommitIds = GetOrCopy<string, string>(ParentCommitIds, source.ParentCommitIds);
            ChangedFiles = GetOrCopy<CommitChangedFile, ICommitChangedFile>(ChangedFiles, source.ChangedFiles);
        }

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

    public partial class CommitChangedFile : CommitFileLink, ICommitChangedFile, IPropertyTarget<ICommitChangedFile>
    {
        public CommitChangedFile()
        {
        }

        public CommitChangedFile(ICommitChangedFile source)
        {
            this.CopyFrom(source);
        }

        public CommitChangedFile(ICommitFileLink source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new CommitChangedFile(((ICommitChangedFile)this));
        }

        public void CopyFrom(ICommitChangedFile source)
        {
            ChangeKind = GetOrCopy<FileChangeKind, FileChangeKind>(ChangeKind, source.ChangeKind);
            OriginalFilePath = GetOrCopy<string, string>(OriginalFilePath, source.OriginalFilePath);
        }

        public FileChangeKind ChangeKind { get; set; }

        public string OriginalFilePath { get; set; }
    }

    public partial class CommitFileLink : EntityBase, ICommitFileLink, IPropertyTarget<ICommitFileLink>
    {
        public CommitFileLink()
        {
        }

        public CommitFileLink(ICommitFileLink source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new CommitFileLink(((ICommitFileLink)this));
        }

        public void CopyFrom(ICommitFileLink source)
        {
            RepoRelativePath = GetOrCopy<string, string>(RepoRelativePath, source.RepoRelativePath);
            FileId = GetOrCopy<string, string>(FileId, source.FileId);
            VersionControlFileId = GetOrCopy<string, string>(VersionControlFileId, source.VersionControlFileId);
        }

        public string RepoRelativePath { get; set; }

        public string FileId { get; set; }

        public string VersionControlFileId { get; set; }
    }

    public partial class Branch : EntityBase, IBranch, IPropertyTarget<IBranch>
    {
        public Branch()
        {
        }

        public Branch(IBranch source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new Branch(((IBranch)this));
        }

        public void CopyFrom(IBranch source)
        {
            Name = GetOrCopy<string, string>(Name, source.Name);
            Description = GetOrCopy<string, string>(Description, source.Description);
            HeadCommitId = GetOrCopy<string, string>(HeadCommitId, source.HeadCommitId);
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string HeadCommitId { get; set; }
    }

    public partial class BoundSourceFile : BoundSourceInfo, IBoundSourceFile, IPropertyTarget<IBoundSourceFile>
    {
        public BoundSourceFile()
        {
        }

        public BoundSourceFile(IBoundSourceInfo source)
        {
            this.CopyFrom(source);
        }

        public BoundSourceFile(IBoundSourceFile source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new BoundSourceFile(((IBoundSourceFile)this));
        }

        public void CopyFrom(IBoundSourceFile source)
        {
            SourceFile = GetOrCopy<SourceFile, ISourceFile>(SourceFile, source.SourceFile);
            Commit = GetOrCopy<Commit, ICommit>(Commit, source.Commit);
            Repo = GetOrCopy<Repository, IRepository>(Repo, source.Repo);
        }

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

    public partial class BoundSourceInfo : EntityBase, IBoundSourceInfo, IPropertyTarget<IBoundSourceInfo>
    {
        public BoundSourceInfo()
        {
        }

        public BoundSourceInfo(IBoundSourceInfo source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new BoundSourceInfo(((IBoundSourceInfo)this));
        }

        public void CopyFrom(IBoundSourceInfo source)
        {
            ReferenceCount = GetOrCopy<int, int>(ReferenceCount, source.ReferenceCount);
            DefinitionCount = GetOrCopy<int, int>(DefinitionCount, source.DefinitionCount);
            Language = GetOrCopy<string, string>(Language, source.Language);
            References = GetOrCopy<ReferenceSpan, IReferenceSpan>(References, source.References);
            Definitions = GetOrCopy<DefinitionSpan, IDefinitionSpan>(Definitions, source.Definitions);
            Classifications = GetOrCopy<ClassificationSpan, IClassificationSpan>(Classifications, source.Classifications);
            OutliningRegions = GetOrCopy<OutliningRegion, IOutliningRegion>(OutliningRegions, source.OutliningRegions);
            ExcludeFromSearch = GetOrCopy<bool, bool>(ExcludeFromSearch, source.ExcludeFromSearch);
        }

        public List<OutliningRegion> OutliningRegions { get; set; } = new();
        IReadOnlyList<IClassificationSpan> IBoundSourceInfo.Classifications
        {
            get
            {
                return Classifications;
            }
        }

        public List<ClassificationSpan> Classifications { get; set; } = new();
        IReadOnlyList<IDefinitionSpan> IBoundSourceInfo.Definitions
        {
            get
            {
                return Definitions;
            }
        }

        public List<DefinitionSpan> Definitions { get; set; } = new();
        IReadOnlyList<IReferenceSpan> IBoundSourceInfo.References
        {
            get
            {
                return References;
            }
        }

        public string Language { get; set; }

        IReadOnlyList<IOutliningRegion> IBoundSourceInfo.OutliningRegions
        {
            get
            {
                return OutliningRegions;
            }
        }

        private Nullable<int> m_DefinitionCount;
        public int DefinitionCount
        {
            get
            {
                return CoerceDefinitionCount(this.m_DefinitionCount);
            }

            set
            {
                this.m_DefinitionCount = value;
            }
        }

        private Nullable<int> m_ReferenceCount;
        public int ReferenceCount
        {
            get
            {
                return CoerceReferenceCount(this.m_ReferenceCount);
            }

            set
            {
                this.m_ReferenceCount = value;
            }
        }

        public List<ReferenceSpan> References { get; set; } = new();
        public bool ExcludeFromSearch { get; set; }
    }

    public partial class SourceControlFileInfo : EntityBase, ISourceControlFileInfo, IPropertyTarget<ISourceControlFileInfo>
    {
        public SourceControlFileInfo()
        {
        }

        public SourceControlFileInfo(ISourceControlFileInfo source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new SourceControlFileInfo(((ISourceControlFileInfo)this));
        }

        public void CopyFrom(ISourceControlFileInfo source)
        {
            SourceControlContentId = GetOrCopy<string, string>(SourceControlContentId, source.SourceControlContentId);
        }

        public string SourceControlContentId { get; set; }
    }

    public partial class SourceFileInfo : RepoFileScopeEntity, ISourceFileInfo, IRepoScopeEntity, ISourceControlFileInfo, IProjectFileScopeEntity, IProjectScopeEntity, IPropertyTarget<ISourceControlFileInfo>, IPropertyTarget<IProjectScopeEntity>, IPropertyTarget<IProjectFileScopeEntity>, IPropertyTarget<ISourceFileInfo>
    {
        public SourceFileInfo()
        {
        }

        public SourceFileInfo(IRepoScopeEntity source)
        {
            this.CopyFrom(source);
        }

        public SourceFileInfo(ISourceControlFileInfo source)
        {
            this.CopyFrom(source);
        }

        public SourceFileInfo(ISourceFileInfo source)
        {
            this.CopyFrom(source);
        }

        public SourceFileInfo(IProjectFileScopeEntity source)
        {
            this.CopyFrom(source);
        }

        public SourceFileInfo(IProjectScopeEntity source)
        {
            this.CopyFrom(source);
        }

        public SourceFileInfo(IRepoFileScopeEntity source)
        {
            this.CopyFrom(source);
        }

        public void CopyFrom(ISourceFileInfo source)
        {
            Lines = GetOrCopy<int, int>(Lines, source.Lines);
            Size = GetOrCopy<int, int>(Size, source.Size);
            Language = GetOrCopy<string, string>(Language, source.Language);
            WebAddress = GetOrCopy<string, string>(WebAddress, source.WebAddress);
            Encoding = GetOrCopy<EncodingDescription, IEncodingDescription>(Encoding, source.Encoding);
            Properties = GetOrCopy<PropertyMap, IPropertyMap>(Properties, source.Properties);
        }

        public void CopyFrom(IProjectFileScopeEntity source)
        {
            ProjectRelativePath = GetOrCopy<string, string>(ProjectRelativePath, source.ProjectRelativePath);
        }

        public void CopyFrom(IProjectScopeEntity source)
        {
            ProjectId = GetOrCopy<string, string>(ProjectId, source.ProjectId);
        }

        public void CopyFrom(ISourceControlFileInfo source)
        {
            SourceControlContentId = GetOrCopy<string, string>(SourceControlContentId, source.SourceControlContentId);
        }

        object IPropertyTarget.CreateClone()
        {
            return new SourceFileInfo(((ISourceFileInfo)this));
        }

        public string SourceControlContentId { get; set; }

        public PropertyMap Properties { get; set; }

        public string ProjectRelativePath { get; set; }

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

        public string ProjectId { get; set; }

        IPropertyMap ISourceFileInfo.Properties
        {
            get
            {
                return Properties;
            }
        }
    }

    public partial class EncodingDescription : EntityBase, IEncodingDescription, IPropertyTarget<IEncodingDescription>
    {
        public EncodingDescription()
        {
        }

        public EncodingDescription(IEncodingDescription source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new EncodingDescription(((IEncodingDescription)this));
        }

        public void CopyFrom(IEncodingDescription source)
        {
            Name = GetOrCopy<string, string>(Name, source.Name);
            Preamble = GetOrCopy<byte[], byte[]>(Preamble, source.Preamble);
        }

        public string Name { get; set; }

        public byte[] Preamble { get; set; }
    }

    public partial class SourceFileBase : EntityBase, ISourceFileBase, IPropertyTarget<ISourceFileBase>
    {
        public SourceFileBase()
        {
        }

        public SourceFileBase(ISourceFileBase source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new SourceFileBase(((ISourceFileBase)this));
        }

        public void CopyFrom(ISourceFileBase source)
        {
            Info = GetOrCopy<SourceFileInfo, ISourceFileInfo>(Info, source.Info);
            ExcludeFromSearch = GetOrCopy<bool, bool>(ExcludeFromSearch, source.ExcludeFromSearch);
        }

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

    public partial class SourceFile : SourceFileBase, ISourceFile, IPropertyTarget<ISourceFile>
    {
        public SourceFile()
        {
        }

        public SourceFile(ISourceFile source)
        {
            this.CopyFrom(source);
        }

        public SourceFile(ISourceFileBase source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new SourceFile(((ISourceFile)this));
        }

        public void CopyFrom(ISourceFile source)
        {
            Content = GetOrCopy<string, string>(Content, source.Content);
        }

        public string Content { get; set; }
    }

    public partial class ChunkedSourceFile : SourceFileBase, IChunkedSourceFile, IPropertyTarget<IChunkedSourceFile>
    {
        public ChunkedSourceFile()
        {
        }

        public ChunkedSourceFile(ISourceFileBase source)
        {
            this.CopyFrom(source);
        }

        public ChunkedSourceFile(IChunkedSourceFile source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new ChunkedSourceFile(((IChunkedSourceFile)this));
        }

        public void CopyFrom(IChunkedSourceFile source)
        {
            Chunks = GetOrCopy<ChunkReference, IChunkReference>(Chunks, source.Chunks);
        }

        public List<ChunkReference> Chunks { get; set; } = new();
        IReadOnlyList<IChunkReference> IChunkedSourceFile.Chunks
        {
            get
            {
                return Chunks;
            }
        }
    }

    public partial class ChunkReference : EntityBase, IChunkReference, IPropertyTarget<IChunkReference>
    {
        public ChunkReference()
        {
        }

        public ChunkReference(IChunkReference source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new ChunkReference(((IChunkReference)this));
        }

        public void CopyFrom(IChunkReference source)
        {
            Id = GetOrCopy<string, string>(Id, source.Id);
            StartLineNumber = GetOrCopy<int, int>(StartLineNumber, source.StartLineNumber);
        }

        public string Id { get; set; }

        public int StartLineNumber { get; set; }
    }

    public partial class SourceFileContentChunk : EntityBase, ISourceFileContentChunk, IPropertyTarget<ISourceFileContentChunk>
    {
        public SourceFileContentChunk()
        {
        }

        public SourceFileContentChunk(ISourceFileContentChunk source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new SourceFileContentChunk(((ISourceFileContentChunk)this));
        }

        public void CopyFrom(ISourceFileContentChunk source)
        {
            ContentLines = GetOrCopy<string, string>(ContentLines, source.ContentLines);
        }

        public List<string> ContentLines { get; set; } = new();
        IReadOnlyList<string> ISourceFileContentChunk.ContentLines
        {
            get
            {
                return ContentLines;
            }
        }
    }

    public partial class OutliningRegion : EntityBase, IOutliningRegion, IPropertyTarget<IOutliningRegion>
    {
        public OutliningRegion()
        {
        }

        public OutliningRegion(IOutliningRegion source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new OutliningRegion(((IOutliningRegion)this));
        }

        public void CopyFrom(IOutliningRegion source)
        {
            Kind = GetOrCopy<string, string>(Kind, source.Kind);
            Header = GetOrCopy<LineSpan, ILineSpan>(Header, source.Header);
            Content = GetOrCopy<LineSpan, ILineSpan>(Content, source.Content);
        }

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

    public partial class DefinitionSpan : Span, IDefinitionSpan, IPropertyTarget<IDefinitionSpan>
    {
        public DefinitionSpan()
        {
        }

        public DefinitionSpan(ISpan source)
        {
            this.CopyFrom(source);
        }

        public DefinitionSpan(IDefinitionSpan source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new DefinitionSpan(((IDefinitionSpan)this));
        }

        public void CopyFrom(IDefinitionSpan source)
        {
            Definition = GetOrCopy<DefinitionSymbol, IDefinitionSymbol>(Definition, source.Definition);
            Parameters = GetOrCopy<ParameterDefinitionSpan, IParameterDefinitionSpan>(Parameters, source.Parameters);
        }

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

    public partial class ParameterDefinitionSpan : LineSpan, IParameterDefinitionSpan, ISpan, IPropertyTarget<IParameterDefinitionSpan>
    {
        public ParameterDefinitionSpan()
        {
        }

        public ParameterDefinitionSpan(ILineSpan source)
        {
            this.CopyFrom(source);
        }

        public ParameterDefinitionSpan(ISpan source)
        {
            this.CopyFrom(source);
        }

        public ParameterDefinitionSpan(IParameterDefinitionSpan source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new ParameterDefinitionSpan(((IParameterDefinitionSpan)this));
        }

        public void CopyFrom(IParameterDefinitionSpan source)
        {
            ParameterIndex = GetOrCopy<int, int>(ParameterIndex, source.ParameterIndex);
            Name = GetOrCopy<string, string>(Name, source.Name);
        }

        public int ParameterIndex { get; set; }

        public string Name { get; set; }
    }

    public partial class ReferenceSpan : SymbolSpan, IReferenceSpan, ITextLineSpan, ILineSpan, ISpan, IPropertyTarget<IReferenceSpan>
    {
        public ReferenceSpan()
        {
        }

        public ReferenceSpan(ILineSpan source)
        {
            this.CopyFrom(source);
        }

        public ReferenceSpan(ISpan source)
        {
            this.CopyFrom(source);
        }

        public ReferenceSpan(IReferenceSpan source)
        {
            this.CopyFrom(source);
        }

        public ReferenceSpan(ITextLineSpan source)
        {
            this.CopyFrom(source);
        }

        public ReferenceSpan(ISymbolSpan source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new ReferenceSpan(((IReferenceSpan)this));
        }

        public void CopyFrom(IReferenceSpan source)
        {
            RelatedDefinition = GetOrCopy<SymbolId, SymbolId>(RelatedDefinition, source.RelatedDefinition);
            Reference = GetOrCopy<ReferenceSymbol, IReferenceSymbol>(Reference, source.Reference);
            Parameters = GetOrCopy<ParameterReferenceSpan, IParameterReferenceSpan>(Parameters, source.Parameters);
        }

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

    public partial class ParameterReferenceSpan : SymbolSpan, IParameterReferenceSpan, ITextLineSpan, ILineSpan, ISpan, IPropertyTarget<IParameterReferenceSpan>
    {
        public ParameterReferenceSpan()
        {
        }

        public ParameterReferenceSpan(ILineSpan source)
        {
            this.CopyFrom(source);
        }

        public ParameterReferenceSpan(ISpan source)
        {
            this.CopyFrom(source);
        }

        public ParameterReferenceSpan(ITextLineSpan source)
        {
            this.CopyFrom(source);
        }

        public ParameterReferenceSpan(ISymbolSpan source)
        {
            this.CopyFrom(source);
        }

        public ParameterReferenceSpan(IParameterReferenceSpan source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new ParameterReferenceSpan(((IParameterReferenceSpan)this));
        }

        public void CopyFrom(IParameterReferenceSpan source)
        {
            ParameterIndex = GetOrCopy<int, int>(ParameterIndex, source.ParameterIndex);
        }

        public int ParameterIndex { get; set; }
    }

    public partial class ClassificationSpan : Span, IClassificationSpan, IPropertyTarget<IClassificationSpan>
    {
        public ClassificationSpan()
        {
        }

        public ClassificationSpan(ISpan source)
        {
            this.CopyFrom(source);
        }

        public ClassificationSpan(IClassificationSpan source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new ClassificationSpan(((IClassificationSpan)this));
        }

        public void CopyFrom(IClassificationSpan source)
        {
            DefaultClassificationColor = GetOrCopy<int, int>(DefaultClassificationColor, source.DefaultClassificationColor);
            Classification = GetOrCopy<string, string>(Classification, source.Classification);
            LocalGroupId = GetOrCopy<int, int>(LocalGroupId, source.LocalGroupId);
        }

        public int DefaultClassificationColor { get; set; }

        public string Classification { get; set; }

        public int LocalGroupId { get; set; }
    }

    public partial class SymbolSpan : TextLineSpan, ISymbolSpan, ILineSpan, ISpan, IPropertyTarget<ISymbolSpan>
    {
        public SymbolSpan()
        {
        }

        public SymbolSpan(ILineSpan source)
        {
            this.CopyFrom(source);
        }

        public SymbolSpan(ISpan source)
        {
            this.CopyFrom(source);
        }

        public SymbolSpan(ITextLineSpan source)
        {
            this.CopyFrom(source);
        }

        public SymbolSpan(ISymbolSpan source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new SymbolSpan(((ISymbolSpan)this));
        }

        public void CopyFrom(ISymbolSpan source)
        {
        }
    }

    public partial class TextLineSpan : LineSpan, ITextLineSpan, ISpan, IPropertyTarget<ITextLineSpan>
    {
        public TextLineSpan()
        {
        }

        public TextLineSpan(ILineSpan source)
        {
            this.CopyFrom(source);
        }

        public TextLineSpan(ISpan source)
        {
            this.CopyFrom(source);
        }

        public TextLineSpan(ITextLineSpan source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new TextLineSpan(((ITextLineSpan)this));
        }

        public void CopyFrom(ITextLineSpan source)
        {
            LineSpanText = GetOrCopy<string, string>(LineSpanText, source.LineSpanText);
        }

        public string LineSpanText { get; set; }
    }

    public partial class LineSpan : Span, ILineSpan, IPropertyTarget<ILineSpan>
    {
        public LineSpan()
        {
        }

        public LineSpan(ILineSpan source)
        {
            this.CopyFrom(source);
        }

        public LineSpan(ISpan source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new LineSpan(((ILineSpan)this));
        }

        public void CopyFrom(ILineSpan source)
        {
            LineIndex = GetOrCopy<int, int>(LineIndex, source.LineIndex);
            LineNumber = GetOrCopy<int, int>(LineNumber, source.LineNumber);
            LineSpanStart = GetOrCopy<int, int>(LineSpanStart, source.LineSpanStart);
            LineOffset = GetOrCopy<int, int>(LineOffset, source.LineOffset);
        }

        public int LineIndex
        {
            get
            {
                return CoerceLineIndex(this.m_LineIndex);
            }

            set
            {
                this.m_LineIndex = value;
            }
        }

        private Nullable<int> m_LineIndex;
        public int LineNumber
        {
            get
            {
                return CoerceLineNumber(this.m_LineNumber);
            }

            set
            {
                this.m_LineNumber = value;
            }
        }

        private Nullable<int> m_LineNumber;
        public int LineSpanStart { get; set; }

        public int LineOffset { get; set; }
    }

    public partial class Span : EntityBase, ISpan, IPropertyTarget<ISpan>
    {
        public Span()
        {
        }

        public Span(ISpan source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new Span(((ISpan)this));
        }

        public void CopyFrom(ISpan source)
        {
            Start = GetOrCopy<int, int>(Start, source.Start);
            Length = GetOrCopy<int, int>(Length, source.Length);
        }

        public int Start { get; set; }

        public int Length { get; set; }
    }

    public partial class CodeReview : EntityBase, ICodeReview, IPropertyTarget<ICodeReview>
    {
        public CodeReview()
        {
        }

        public CodeReview(ICodeReview source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new CodeReview(((ICodeReview)this));
        }

        public void CopyFrom(ICodeReview source)
        {
            Id = GetOrCopy<string, string>(Id, source.Id);
            Description = GetOrCopy<string, string>(Description, source.Description);
            Url = GetOrCopy<string, string>(Url, source.Url);
            Status = GetOrCopy<CodeReviewStatus, CodeReviewStatus>(Status, source.Status);
        }

        public string Id { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public CodeReviewStatus Status { get; set; }
    }

    public partial class CodeReviewIteration : EntityBase, ICodeReviewIteration, IPropertyTarget<ICodeReviewIteration>
    {
        public CodeReviewIteration()
        {
        }

        public CodeReviewIteration(ICodeReviewIteration source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new CodeReviewIteration(((ICodeReviewIteration)this));
        }

        public void CopyFrom(ICodeReviewIteration source)
        {
            IterationNumber = GetOrCopy<int, int>(IterationNumber, source.IterationNumber);
            ReviewId = GetOrCopy<string, string>(ReviewId, source.ReviewId);
            Description = GetOrCopy<string, string>(Description, source.Description);
            Files = GetOrCopy<CodeReviewFile, ICodeReviewFile>(Files, source.Files);
        }

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

    public partial class CodeReviewerInfo : EntityBase, ICodeReviewerInfo, IPropertyTarget<ICodeReviewerInfo>
    {
        public CodeReviewerInfo()
        {
        }

        public CodeReviewerInfo(ICodeReviewerInfo source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new CodeReviewerInfo(((ICodeReviewerInfo)this));
        }

        public void CopyFrom(ICodeReviewerInfo source)
        {
            Name = GetOrCopy<string, string>(Name, source.Name);
        }

        public string Name { get; set; }
    }

    public partial class CodeReviewFile : EntityBase, ICodeReviewFile, IPropertyTarget<ICodeReviewFile>
    {
        public CodeReviewFile()
        {
        }

        public CodeReviewFile(ICodeReviewFile source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new CodeReviewFile(((ICodeReviewFile)this));
        }

        public void CopyFrom(ICodeReviewFile source)
        {
            StartIteration = GetOrCopy<int, int>(StartIteration, source.StartIteration);
            RepoRelativePath = GetOrCopy<string, string>(RepoRelativePath, source.RepoRelativePath);
            FileId = GetOrCopy<string, string>(FileId, source.FileId);
            BaselineFileId = GetOrCopy<string, string>(BaselineFileId, source.BaselineFileId);
            ChangeKind = GetOrCopy<FileChangeKind, FileChangeKind>(ChangeKind, source.ChangeKind);
        }

        public int StartIteration { get; set; }

        public string RepoRelativePath { get; set; }

        public string FileId { get; set; }

        public string BaselineFileId { get; set; }

        public FileChangeKind ChangeKind { get; set; }
    }

    public partial class CodeReviewCommentThread : EntityBase, ICodeReviewCommentThread, IPropertyTarget<ICodeReviewCommentThread>
    {
        public CodeReviewCommentThread()
        {
        }

        public CodeReviewCommentThread(ICodeReviewCommentThread source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new CodeReviewCommentThread(((ICodeReviewCommentThread)this));
        }

        public void CopyFrom(ICodeReviewCommentThread source)
        {
            OriginalSpan = GetOrCopy<LineSpan, ILineSpan>(OriginalSpan, source.OriginalSpan);
            StartIteration = GetOrCopy<int, int>(StartIteration, source.StartIteration);
            LastUpdated = GetOrCopy<DateTime, DateTime>(LastUpdated, source.LastUpdated);
            FileRepoRelativePath = GetOrCopy<string, string>(FileRepoRelativePath, source.FileRepoRelativePath);
            Comments = GetOrCopy<CodeReviewComment, ICodeReviewComment>(Comments, source.Comments);
        }

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

    public partial class CodeReviewComment : EntityBase, ICodeReviewComment, IPropertyTarget<ICodeReviewComment>
    {
        public CodeReviewComment()
        {
        }

        public CodeReviewComment(ICodeReviewComment source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new CodeReviewComment(((ICodeReviewComment)this));
        }

        public void CopyFrom(ICodeReviewComment source)
        {
            Text = GetOrCopy<string, string>(Text, source.Text);
            Reviewer = GetOrCopy<string, string>(Reviewer, source.Reviewer);
            Importance = GetOrCopy<CommentImportance, CommentImportance>(Importance, source.Importance);
            CommentTime = GetOrCopy<DateTime, DateTime>(CommentTime, source.CommentTime);
        }

        public string Text { get; set; }

        public string Reviewer { get; set; }

        public CommentImportance Importance { get; set; }

        public DateTime CommentTime { get; set; }
    }

    public partial class AnalyzedProject : ProjectScopeEntity, IAnalyzedProject, IRepoScopeEntity, IPropertyTarget<IAnalyzedProject>
    {
        public AnalyzedProject()
        {
        }

        public AnalyzedProject(IRepoScopeEntity source)
        {
            this.CopyFrom(source);
        }

        public AnalyzedProject(IProjectScopeEntity source)
        {
            this.CopyFrom(source);
        }

        public AnalyzedProject(IAnalyzedProject source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new AnalyzedProject(((IAnalyzedProject)this));
        }

        public void CopyFrom(IAnalyzedProject source)
        {
            ProjectKind = GetOrCopy<string, string>(ProjectKind, source.ProjectKind);
            PrimaryFile = GetOrCopy<ProjectFileLink, IProjectFileLink>(PrimaryFile, source.PrimaryFile);
            Files = GetOrCopy<ProjectFileLink, IProjectFileLink>(Files, source.Files);
            ProjectReferences = GetOrCopy<ReferencedProject, IReferencedProject>(ProjectReferences, source.ProjectReferences);
        }

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

    public partial class ReferencedProject : EntityBase, IReferencedProject, IPropertyTarget<IReferencedProject>
    {
        public ReferencedProject()
        {
        }

        public ReferencedProject(IReferencedProject source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new ReferencedProject(((IReferencedProject)this));
        }

        public void CopyFrom(IReferencedProject source)
        {
            ProjectId = GetOrCopy<string, string>(ProjectId, source.ProjectId);
            Definitions = GetOrCopy<DefinitionSymbol, IDefinitionSymbol>(Definitions, source.Definitions);
            DisplayName = GetOrCopy<string, string>(DisplayName, source.DisplayName);
            Properties = GetOrCopy<PropertyMap, IPropertyMap>(Properties, source.Properties);
        }

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

    public partial class ProjectFileLink : ProjectFileScopeEntity, IProjectFileLink, IRepoFileScopeEntity, IRepoScopeEntity, IProjectScopeEntity, IPropertyTarget<IProjectFileLink>
    {
        public ProjectFileLink()
        {
        }

        public ProjectFileLink(IProjectFileLink source)
        {
            this.CopyFrom(source);
        }

        public ProjectFileLink(IRepoScopeEntity source)
        {
            this.CopyFrom(source);
        }

        public ProjectFileLink(IProjectScopeEntity source)
        {
            this.CopyFrom(source);
        }

        public ProjectFileLink(IRepoFileScopeEntity source)
        {
            this.CopyFrom(source);
        }

        public ProjectFileLink(IProjectFileScopeEntity source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new ProjectFileLink(((IProjectFileLink)this));
        }

        public void CopyFrom(IProjectFileLink source)
        {
            FileId = GetOrCopy<string, string>(FileId, source.FileId);
        }

        public string FileId { get; set; }
    }

    public partial class PropertySearchModel : SearchEntity, IPropertySearchModel, ISearchEntityBase, IPropertyTarget<IPropertySearchModel>
    {
        public PropertySearchModel()
        {
        }

        public PropertySearchModel(ISearchEntity source)
        {
            this.CopyFrom(source);
        }

        public PropertySearchModel(ISearchEntityBase source)
        {
            this.CopyFrom(source);
        }

        public PropertySearchModel(IPropertySearchModel source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new PropertySearchModel(((IPropertySearchModel)this));
        }

        public void CopyFrom(IPropertySearchModel source)
        {
            Key = GetOrCopy<string, string>(Key, source.Key);
            Value = GetOrCopy<string, string>(Value, source.Value);
            OwnerId = GetOrCopy<string, string>(OwnerId, source.OwnerId);
        }

        public string Key { get; set; }

        public string Value { get; set; }

        public string OwnerId { get; set; }
    }

    public partial class Repository : EntityBase, IRepository, IPropertyTarget<IRepository>
    {
        public Repository()
        {
        }

        public Repository(IRepository source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new Repository(((IRepository)this));
        }

        public void CopyFrom(IRepository source)
        {
            Name = GetOrCopy<string, string>(Name, source.Name);
            Description = GetOrCopy<string, string>(Description, source.Description);
            SourceControlWebAddress = GetOrCopy<string, string>(SourceControlWebAddress, source.SourceControlWebAddress);
            FileWebAddressTransformInputRegex = GetOrCopy<string, string>(FileWebAddressTransformInputRegex, source.FileWebAddressTransformInputRegex);
            PrimaryBranch = GetOrCopy<string, string>(PrimaryBranch, source.PrimaryBranch);
            RepositoryReferences = GetOrCopy<RepositoryReference, IRepositoryReference>(RepositoryReferences, source.RepositoryReferences);
        }

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

    public partial class RepositoryReference : EntityBase, IRepositoryReference, IPropertyTarget<IRepositoryReference>
    {
        public RepositoryReference()
        {
        }

        public RepositoryReference(IRepositoryReference source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new RepositoryReference(((IRepositoryReference)this));
        }

        public void CopyFrom(IRepositoryReference source)
        {
            Name = GetOrCopy<string, string>(Name, source.Name);
            Id = GetOrCopy<string, string>(Id, source.Id);
        }

        public string Name { get; set; }

        public string Id { get; set; }
    }

    public partial class RegisteredEntity : SearchEntity, IRegisteredEntity, ISearchEntityBase, IPropertyTarget<IRegisteredEntity>
    {
        public RegisteredEntity()
        {
        }

        public RegisteredEntity(ISearchEntity source)
        {
            this.CopyFrom(source);
        }

        public RegisteredEntity(ISearchEntityBase source)
        {
            this.CopyFrom(source);
        }

        public RegisteredEntity(IRegisteredEntity source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new RegisteredEntity(((IRegisteredEntity)this));
        }

        public void CopyFrom(IRegisteredEntity source)
        {
            DateAdded = GetOrCopy<DateTime, DateTime>(DateAdded, source.DateAdded);
            EntityUid = GetOrCopy<string, string>(EntityUid, source.EntityUid);
            IndexName = GetOrCopy<string, string>(IndexName, source.IndexName);
        }

        public DateTime DateAdded { get; set; }

        public string EntityUid { get; set; }

        public string IndexName { get; set; }
    }

    public partial class StoredFilter : SearchEntity, IStoredFilter, ISearchEntityBase, IPropertyTarget<IStoredFilter>
    {
        public StoredFilter()
        {
        }

        public StoredFilter(ISearchEntity source)
        {
            this.CopyFrom(source);
        }

        public StoredFilter(IStoredFilter source)
        {
            this.CopyFrom(source);
        }

        public StoredFilter(ISearchEntityBase source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new StoredFilter(((IStoredFilter)this));
        }

        public void CopyFrom(IStoredFilter source)
        {
            DateUpdated = GetOrCopy<DateTime, DateTime>(DateUpdated, source.DateUpdated);
            FullPath = GetOrCopy<string, string>(FullPath, source.FullPath);
            Name = GetOrCopy<string, string>(Name, source.Name);
            IndexName = GetOrCopy<string, string>(IndexName, source.IndexName);
            StableIds = GetOrCopy<byte[], byte[]>(StableIds, source.StableIds);
            Children = GetOrCopy<ChildFilterReference, IChildFilterReference>(Children, source.Children);
            FilterHash = GetOrCopy<string, string>(FilterHash, source.FilterHash);
            Cardinality = GetOrCopy<int, int>(Cardinality, source.Cardinality);
        }

        public DateTime DateUpdated { get; set; }

        public string FullPath { get; set; }

        public string Name { get; set; }

        public string IndexName { get; set; }

        public byte[] StableIds { get; set; }

        public List<ChildFilterReference> Children { get; set; } = new();
        IReadOnlyList<IChildFilterReference> IStoredFilter.Children
        {
            get
            {
                return Children;
            }
        }

        public string FilterHash { get; set; }

        public int Cardinality { get; set; }
    }

    public partial class ChildFilterReference : EntityBase, IChildFilterReference, IPropertyTarget<IChildFilterReference>
    {
        public ChildFilterReference()
        {
        }

        public ChildFilterReference(IChildFilterReference source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new ChildFilterReference(((IChildFilterReference)this));
        }

        public void CopyFrom(IChildFilterReference source)
        {
            FullPath = GetOrCopy<string, string>(FullPath, source.FullPath);
            Uid = GetOrCopy<string, string>(Uid, source.Uid);
            StableIds = GetOrCopy<byte[], byte[]>(StableIds, source.StableIds);
            Cardinality = GetOrCopy<int, int>(Cardinality, source.Cardinality);
        }

        public string FullPath { get; set; }

        public string Uid { get; set; }

        public byte[] StableIds { get; set; }

        public int Cardinality { get; set; }
    }

    public partial class DefinitionSearchModel : SearchEntity, IDefinitionSearchModel, ISearchEntityBase, IPropertyTarget<IDefinitionSearchModel>
    {
        public DefinitionSearchModel()
        {
        }

        public DefinitionSearchModel(ISearchEntity source)
        {
            this.CopyFrom(source);
        }

        public DefinitionSearchModel(ISearchEntityBase source)
        {
            this.CopyFrom(source);
        }

        public DefinitionSearchModel(IDefinitionSearchModel source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new DefinitionSearchModel(((IDefinitionSearchModel)this));
        }

        public void CopyFrom(IDefinitionSearchModel source)
        {
            Definition = GetOrCopy<DefinitionSymbol, IDefinitionSymbol>(Definition, source.Definition);
            Keywords = GetOrCopy<string, string>(Keywords, source.Keywords);
        }

        public DefinitionSymbol Definition { get; set; }

        IDefinitionSymbol IDefinitionSearchModel.Definition
        {
            get
            {
                return Definition;
            }
        }

        public List<string> Keywords { get; set; } = new();
        IReadOnlyList<string> IDefinitionSearchModel.Keywords
        {
            get
            {
                return Keywords;
            }
        }
    }

    public partial class LanguageSearchModel : SearchEntity, ILanguageSearchModel, ISearchEntityBase, IPropertyTarget<ILanguageSearchModel>
    {
        public LanguageSearchModel()
        {
        }

        public LanguageSearchModel(ISearchEntity source)
        {
            this.CopyFrom(source);
        }

        public LanguageSearchModel(ISearchEntityBase source)
        {
            this.CopyFrom(source);
        }

        public LanguageSearchModel(ILanguageSearchModel source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new LanguageSearchModel(((ILanguageSearchModel)this));
        }

        public void CopyFrom(ILanguageSearchModel source)
        {
            Language = GetOrCopy<LanguageInfo, ILanguageInfo>(Language, source.Language);
        }

        public LanguageInfo Language { get; set; }

        ILanguageInfo ILanguageSearchModel.Language
        {
            get
            {
                return Language;
            }
        }
    }

    public partial class ReferenceSearchModel : ProjectFileScopeEntity, IReferenceSearchModel, IRepoFileScopeEntity, IRepoScopeEntity, IProjectScopeEntity, ISearchEntity, ISearchEntityBase, IPropertyTarget<ISearchEntity>, IPropertyTarget<ISearchEntityBase>, IPropertyTarget<IReferenceSearchModel>
    {
        public ReferenceSearchModel()
        {
        }

        public ReferenceSearchModel(IReferenceSearchModel source)
        {
            this.CopyFrom(source);
        }

        public ReferenceSearchModel(IProjectFileScopeEntity source)
        {
            this.CopyFrom(source);
        }

        public ReferenceSearchModel(IRepoFileScopeEntity source)
        {
            this.CopyFrom(source);
        }

        public ReferenceSearchModel(ISearchEntityBase source)
        {
            this.CopyFrom(source);
        }

        public ReferenceSearchModel(IRepoScopeEntity source)
        {
            this.CopyFrom(source);
        }

        public ReferenceSearchModel(IProjectScopeEntity source)
        {
            this.CopyFrom(source);
        }

        public ReferenceSearchModel(ISearchEntity source)
        {
            this.CopyFrom(source);
        }

        public void CopyFrom(IReferenceSearchModel source)
        {
            Reference = GetOrCopy<ReferenceSymbol, IReferenceSymbol>(Reference, source.Reference);
            Spans = GetOrCopy<SymbolSpan, ISymbolSpan>(Spans, source.Spans);
            CompressedSpans = GetOrCopy<SymbolLineSpanList, ISymbolLineSpanList>(CompressedSpans, source.CompressedSpans);
        }

        public void CopyFrom(ISearchEntityBase source)
        {
        }

        public void CopyFrom(ISearchEntity source)
        {
            Uid = GetOrCopy<string, string>(Uid, source.Uid);
            EntityContentId = GetOrCopy<string, string>(EntityContentId, source.EntityContentId);
            EntityContentSize = GetOrCopy<int, int>(EntityContentSize, source.EntityContentSize);
            EntityVersion = GetOrCopy<Nullable<long>, Nullable<long>>(EntityVersion, source.EntityVersion);
            RoutingGroup = GetOrCopy<int, int>(RoutingGroup, source.RoutingGroup);
            StableId = GetOrCopy<int, int>(StableId, source.StableId);
            SortKey = GetOrCopy<string, string>(SortKey, source.SortKey);
        }

        object IPropertyTarget.CreateClone()
        {
            return new ReferenceSearchModel(((IReferenceSearchModel)this));
        }

        IReadOnlyList<ISymbolSpan> IReferenceSearchModel.Spans
        {
            get
            {
                return Spans;
            }
        }

        private List<SymbolSpan> m_Spans;
        public List<SymbolSpan> Spans
        {
            get
            {
                return CoerceSpans(this.m_Spans);
            }

            set
            {
                this.m_Spans = value;
            }
        }

        IReferenceSymbol IReferenceSearchModel.Reference
        {
            get
            {
                return Reference;
            }
        }

        public ReferenceSymbol Reference { get; set; }

        public Nullable<long> EntityVersion { get; set; }

        public string Uid { get; set; }

        public SymbolLineSpanList CompressedSpans { get; set; }

        public string EntityContentId { get; set; }

        public string SortKey { get; set; }

        public int StableId { get; set; }

        public int RoutingGroup { get; set; }

        public int EntityContentSize { get; set; }

        ISymbolLineSpanList IReferenceSearchModel.CompressedSpans
        {
            get
            {
                return CompressedSpans;
            }
        }
    }

    public partial class SourceSearchModelBase : SearchEntity, ISourceSearchModelBase, ISearchEntityBase, IPropertyTarget<ISourceSearchModelBase>
    {
        public SourceSearchModelBase()
        {
        }

        public SourceSearchModelBase(ISearchEntity source)
        {
            this.CopyFrom(source);
        }

        public SourceSearchModelBase(ISearchEntityBase source)
        {
            this.CopyFrom(source);
        }

        public SourceSearchModelBase(ISourceSearchModelBase source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new SourceSearchModelBase(((ISourceSearchModelBase)this));
        }

        public void CopyFrom(ISourceSearchModelBase source)
        {
            SourceControlInfo = GetOrCopy<SourceControlFileInfo, ISourceControlFileInfo>(SourceControlInfo, source.SourceControlInfo);
            File = GetOrCopy<ChunkedSourceFile, IChunkedSourceFile>(File, source.File);
        }

        public SourceControlFileInfo SourceControlInfo { get; set; }

        ISourceControlFileInfo ISourceSearchModelBase.SourceControlInfo
        {
            get
            {
                return SourceControlInfo;
            }
        }

        public ChunkedSourceFile File { get; set; }

        IChunkedSourceFile ISourceSearchModelBase.File
        {
            get
            {
                return File;
            }
        }
    }

    public partial class BoundSourceSearchModel : SourceSearchModelBase, IBoundSourceSearchModel, ISearchEntity, ISearchEntityBase, IPropertyTarget<IBoundSourceSearchModel>
    {
        public BoundSourceSearchModel()
        {
        }

        public BoundSourceSearchModel(ISearchEntity source)
        {
            this.CopyFrom(source);
        }

        public BoundSourceSearchModel(ISearchEntityBase source)
        {
            this.CopyFrom(source);
        }

        public BoundSourceSearchModel(ISourceSearchModelBase source)
        {
            this.CopyFrom(source);
        }

        public BoundSourceSearchModel(IBoundSourceSearchModel source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new BoundSourceSearchModel(((IBoundSourceSearchModel)this));
        }

        public void CopyFrom(IBoundSourceSearchModel source)
        {
            TextUid = GetOrCopy<string, string>(TextUid, source.TextUid);
            BindingInfo = GetOrCopy<BoundSourceInfo, IBoundSourceInfo>(BindingInfo, source.BindingInfo);
            CompressedClassifications = GetOrCopy<ClassificationList, IClassificationList>(CompressedClassifications, source.CompressedClassifications);
            CompressedReferences = GetOrCopy<ReferenceList, IReferenceList>(CompressedReferences, source.CompressedReferences);
        }

        public string TextUid { get; set; }

        public BoundSourceInfo BindingInfo { get; set; }

        IBoundSourceInfo IBoundSourceSearchModel.BindingInfo
        {
            get
            {
                return BindingInfo;
            }
        }

        public ClassificationList CompressedClassifications { get; set; }

        IClassificationList IBoundSourceSearchModel.CompressedClassifications
        {
            get
            {
                return CompressedClassifications;
            }
        }

        public ReferenceList CompressedReferences { get; set; }

        IReferenceList IBoundSourceSearchModel.CompressedReferences
        {
            get
            {
                return CompressedReferences;
            }
        }
    }

    public partial class TextSourceSearchModel : SourceSearchModelBase, ITextSourceSearchModel, ISearchEntity, ISearchEntityBase, IPropertyTarget<ITextSourceSearchModel>
    {
        public TextSourceSearchModel()
        {
        }

        public TextSourceSearchModel(ISearchEntity source)
        {
            this.CopyFrom(source);
        }

        public TextSourceSearchModel(ISearchEntityBase source)
        {
            this.CopyFrom(source);
        }

        public TextSourceSearchModel(ITextSourceSearchModel source)
        {
            this.CopyFrom(source);
        }

        public TextSourceSearchModel(ISourceSearchModelBase source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new TextSourceSearchModel(((ITextSourceSearchModel)this));
        }

        public void CopyFrom(ITextSourceSearchModel source)
        {
        }
    }

    public partial class TextChunkSearchModel : SearchEntity, ITextChunkSearchModel, ISearchEntityBase, IPropertyTarget<ITextChunkSearchModel>
    {
        public TextChunkSearchModel()
        {
        }

        public TextChunkSearchModel(ISearchEntity source)
        {
            this.CopyFrom(source);
        }

        public TextChunkSearchModel(ISearchEntityBase source)
        {
            this.CopyFrom(source);
        }

        public TextChunkSearchModel(ITextChunkSearchModel source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new TextChunkSearchModel(((ITextChunkSearchModel)this));
        }

        public void CopyFrom(ITextChunkSearchModel source)
        {
            Chunk = GetOrCopy<SourceFileContentChunk, ISourceFileContentChunk>(Chunk, source.Chunk);
            RawChunk = GetOrCopy<SourceFileContentChunk, ISourceFileContentChunk>(RawChunk, source.RawChunk);
        }

        public SourceFileContentChunk Chunk { get; set; }

        ISourceFileContentChunk ITextChunkSearchModel.Chunk
        {
            get
            {
                return Chunk;
            }
        }

        public SourceFileContentChunk RawChunk { get; set; }

        ISourceFileContentChunk ITextChunkSearchModel.RawChunk
        {
            get
            {
                return RawChunk;
            }
        }
    }

    public partial class RepositorySearchModel : SearchEntity, IRepositorySearchModel, ISearchEntityBase, IPropertyTarget<IRepositorySearchModel>
    {
        public RepositorySearchModel()
        {
        }

        public RepositorySearchModel(ISearchEntity source)
        {
            this.CopyFrom(source);
        }

        public RepositorySearchModel(IRepositorySearchModel source)
        {
            this.CopyFrom(source);
        }

        public RepositorySearchModel(ISearchEntityBase source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new RepositorySearchModel(((IRepositorySearchModel)this));
        }

        public void CopyFrom(IRepositorySearchModel source)
        {
            Repository = GetOrCopy<Repository, IRepository>(Repository, source.Repository);
        }

        public Repository Repository { get; set; }

        IRepository IRepositorySearchModel.Repository
        {
            get
            {
                return Repository;
            }
        }
    }

    public partial class ProjectSearchModel : SearchEntity, IProjectSearchModel, ISearchEntityBase, IPropertyTarget<IProjectSearchModel>
    {
        public ProjectSearchModel()
        {
        }

        public ProjectSearchModel(ISearchEntity source)
        {
            this.CopyFrom(source);
        }

        public ProjectSearchModel(ISearchEntityBase source)
        {
            this.CopyFrom(source);
        }

        public ProjectSearchModel(IProjectSearchModel source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new ProjectSearchModel(((IProjectSearchModel)this));
        }

        public void CopyFrom(IProjectSearchModel source)
        {
            Project = GetOrCopy<AnalyzedProject, IAnalyzedProject>(Project, source.Project);
        }

        public AnalyzedProject Project { get; set; }

        IAnalyzedProject IProjectSearchModel.Project
        {
            get
            {
                return Project;
            }
        }
    }

    public partial class ProjectReferenceSearchModel : ProjectScopeEntity, IProjectReferenceSearchModel, IRepoScopeEntity, ISearchEntity, ISearchEntityBase, IPropertyTarget<ISearchEntity>, IPropertyTarget<ISearchEntityBase>, IPropertyTarget<IProjectReferenceSearchModel>
    {
        public ProjectReferenceSearchModel()
        {
        }

        public ProjectReferenceSearchModel(ISearchEntity source)
        {
            this.CopyFrom(source);
        }

        public ProjectReferenceSearchModel(IProjectReferenceSearchModel source)
        {
            this.CopyFrom(source);
        }

        public ProjectReferenceSearchModel(ISearchEntityBase source)
        {
            this.CopyFrom(source);
        }

        public ProjectReferenceSearchModel(IProjectScopeEntity source)
        {
            this.CopyFrom(source);
        }

        public ProjectReferenceSearchModel(IRepoScopeEntity source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new ProjectReferenceSearchModel(((IProjectReferenceSearchModel)this));
        }

        public void CopyFrom(ISearchEntity source)
        {
            Uid = GetOrCopy<string, string>(Uid, source.Uid);
            EntityContentId = GetOrCopy<string, string>(EntityContentId, source.EntityContentId);
            EntityContentSize = GetOrCopy<int, int>(EntityContentSize, source.EntityContentSize);
            EntityVersion = GetOrCopy<Nullable<long>, Nullable<long>>(EntityVersion, source.EntityVersion);
            RoutingGroup = GetOrCopy<int, int>(RoutingGroup, source.RoutingGroup);
            StableId = GetOrCopy<int, int>(StableId, source.StableId);
            SortKey = GetOrCopy<string, string>(SortKey, source.SortKey);
        }

        public void CopyFrom(IProjectReferenceSearchModel source)
        {
            ProjectReference = GetOrCopy<ReferencedProject, IReferencedProject>(ProjectReference, source.ProjectReference);
        }

        public void CopyFrom(ISearchEntityBase source)
        {
        }

        public int StableId { get; set; }

        public ReferencedProject ProjectReference { get; set; }

        public int RoutingGroup { get; set; }

        public Nullable<long> EntityVersion { get; set; }

        public int EntityContentSize { get; set; }

        public string EntityContentId { get; set; }

        public string Uid { get; set; }

        public string SortKey { get; set; }

        IReferencedProject IProjectReferenceSearchModel.ProjectReference
        {
            get
            {
                return ProjectReference;
            }
        }
    }

    public partial class CommitSearchModel : SearchEntity, ICommitSearchModel, ISearchEntityBase, IPropertyTarget<ICommitSearchModel>
    {
        public CommitSearchModel()
        {
        }

        public CommitSearchModel(ISearchEntity source)
        {
            this.CopyFrom(source);
        }

        public CommitSearchModel(ICommitSearchModel source)
        {
            this.CopyFrom(source);
        }

        public CommitSearchModel(ISearchEntityBase source)
        {
            this.CopyFrom(source);
        }

        object IPropertyTarget.CreateClone()
        {
            return new CommitSearchModel(((ICommitSearchModel)this));
        }

        public void CopyFrom(ICommitSearchModel source)
        {
            Commit = GetOrCopy<Commit, ICommit>(Commit, source.Commit);
        }

        public Commit Commit { get; set; }

        ICommit ICommitSearchModel.Commit
        {
            get
            {
                return Commit;
            }
        }
    }

    public partial class CommitFilesSearchModel : CommitScopeEntity, ICommitFilesSearchModel, IRepoScopeEntity, ISearchEntity, ISearchEntityBase, IPropertyTarget<ISearchEntity>, IPropertyTarget<ICommitFilesSearchModel>, IPropertyTarget<ISearchEntityBase>
    {
        public CommitFilesSearchModel()
        {
        }

        public CommitFilesSearchModel(ISearchEntityBase source)
        {
            this.CopyFrom(source);
        }

        public CommitFilesSearchModel(ICommitFilesSearchModel source)
        {
            this.CopyFrom(source);
        }

        public CommitFilesSearchModel(IRepoScopeEntity source)
        {
            this.CopyFrom(source);
        }

        public CommitFilesSearchModel(ICommitScopeEntity source)
        {
            this.CopyFrom(source);
        }

        public CommitFilesSearchModel(ISearchEntity source)
        {
            this.CopyFrom(source);
        }

        public void CopyFrom(ISearchEntityBase source)
        {
        }

        public void CopyFrom(ISearchEntity source)
        {
            Uid = GetOrCopy<string, string>(Uid, source.Uid);
            EntityContentId = GetOrCopy<string, string>(EntityContentId, source.EntityContentId);
            EntityContentSize = GetOrCopy<int, int>(EntityContentSize, source.EntityContentSize);
            EntityVersion = GetOrCopy<Nullable<long>, Nullable<long>>(EntityVersion, source.EntityVersion);
            RoutingGroup = GetOrCopy<int, int>(RoutingGroup, source.RoutingGroup);
            StableId = GetOrCopy<int, int>(StableId, source.StableId);
            SortKey = GetOrCopy<string, string>(SortKey, source.SortKey);
        }

        public void CopyFrom(ICommitFilesSearchModel source)
        {
            CommitFiles = GetOrCopy<CommitFileLink, ICommitFileLink>(CommitFiles, source.CommitFiles);
        }

        object IPropertyTarget.CreateClone()
        {
            return new CommitFilesSearchModel(((ICommitFilesSearchModel)this));
        }

        public string EntityContentId { get; set; }

        public int EntityContentSize { get; set; }

        public Nullable<long> EntityVersion { get; set; }

        public int RoutingGroup { get; set; }

        public string SortKey { get; set; }

        public List<CommitFileLink> CommitFiles { get; set; } = new();
        IReadOnlyList<ICommitFileLink> ICommitFilesSearchModel.CommitFiles
        {
            get
            {
                return CommitFiles;
            }
        }

        public string Uid { get; set; }

        public int StableId { get; set; }
    }
}