using System.Diagnostics.Contracts;

namespace Codex.ObjectModel.Implementation
{
    public partial class AnalyzedProject
    {
        public AnalyzedProject(string repositoryName, string projectId)
        {
            RepositoryName = repositoryName;
            ProjectId = projectId;
        }

        /// <summary>
        /// Additional source files to add to the repository
        /// </summary>
        public List<BoundSourceFile> AdditionalSourceFiles { get; set; } = new List<BoundSourceFile>();
    }
}
