namespace Codex.ObjectModel
{
    public interface IRepository
    {
        /// <summary>
        /// The name of the repository
        /// </summary>
        [SearchBehavior(SearchBehavior.NormalizedKeyword)]
        string Name { get; }

        /// <summary>
        /// Describes the repository
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The web address for source control of the repository
        /// </summary>
        string SourceControlWebAddress { get; }

        /// <summary>
        /// The regex for extracting
        /// </summary>
        string FileWebAddressTransformInputRegex { get; }

        /// <summary>
        /// The name of the primary branch for the repository
        /// </summary>
        string PrimaryBranch { get; }

        IReadOnlyList<IRepositoryReference> RepositoryReferences { get; }
    }

    public interface IRepositoryReference
    {
        /// <summary>
        /// The name of the reference repository
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Optional. Id of repository
        /// </summary>
        string Id { get; }
    }
}
