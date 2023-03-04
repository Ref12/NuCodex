namespace Codex.ObjectModel
{
    class Repository
    {
        string Name
        {
            get;
            set;
        }

        string Description
        {
            get;
            set;
        }

        string SourceControlWebAddress
        {
            get;
            set;
        }

        string FileWebAddressTransformInputRegex
        {
            get;
            set;
        }

        string PrimaryBranch
        {
            get;
            set;
        }

        global::System.Collections.Generic.IReadOnlyList<global::Codex.ObjectModel.IRepositoryReference> RepositoryReferences
        {
            get;
            set;
        }
    }

    class RepositoryReference
    {
        string Name
        {
            get;
            set;
        }

        string Id
        {
            get;
            set;
        }
    }

    class RepoScopeEntity
    {
        string RepositoryName
        {
            get;
            set;
        }
    }

    class CommitScopeEntity
    {
        string CommitId
        {
            get;
            set;
        }
    }

    class ProjectScopeEntity
    {
        string ProjectId
        {
            get;
            set;
        }
    }

    class RepoFileScopeEntity
    {
        string RepoRelativePath
        {
            get;
            set;
        }
    }

    class ProjectFileScopeEntity
    {
        string ProjectRelativePath
        {
            get;
            set;
        }
    }
}
