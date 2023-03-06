namespace Codex.ObjectModel.Implementation
{
    public class Repository : IRepository
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string SourceControlWebAddress { get; set; }

        public string FileWebAddressTransformInputRegex { get; set; }

        public string PrimaryBranch { get; set; }

        public List<RepositoryReference> RepositoryReferences { get; set; }

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
}