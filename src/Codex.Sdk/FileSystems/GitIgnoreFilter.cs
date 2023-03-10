using System.Collections.Concurrent;

namespace Codex.Utilities
{
    public class GitIgnoreFilter : FileSystemFilter
    {
        public ConcurrentDictionary<string, KeyValuePair<string, GitIgnore>> gitIgnoreMap = new ConcurrentDictionary<string, KeyValuePair<string, GitIgnore>>(StringComparer.OrdinalIgnoreCase);

        private static readonly char[] PathSeparators = new char[] { '\\', '/' };

        private static readonly string[] DefaultGitIgnoreFileNames = new string[] { ".gitignore", ".tfignore" };

        private readonly string[] gitIgnoreFileNames;

        public GitIgnoreFilter(params string[] gitIgnoreFileNames)
        {
            if (gitIgnoreFileNames == null || gitIgnoreFileNames.Length == 0)
            {
                this.gitIgnoreFileNames = DefaultGitIgnoreFileNames;
            }
            else
            {
                this.gitIgnoreFileNames = gitIgnoreFileNames;
            }
        }

        public override bool IncludeDirectory(FileSystem fileSystem, string directoryPath)
        {
            string gitIgnoreDirectoryPath = directoryPath;
            GitIgnore gitIgnore;
            PopulateGitIgnore(fileSystem, ref gitIgnoreDirectoryPath, out gitIgnore);

            var result = Include(directoryPath, gitIgnoreDirectoryPath ?? string.Empty, gitIgnore, isDirectory: true);
            return result;
        }

        private void PopulateGitIgnore(FileSystem fileSystem, ref string directoryPath, out GitIgnore gitIgnore)
        {
            directoryPath = directoryPath.TrimEnd(PathSeparators);
            var originalDirectoryPath = directoryPath;
            gitIgnore = null;

            while (!string.IsNullOrEmpty(directoryPath))
            {
                if (GetGitIgnore(fileSystem, ref directoryPath, out gitIgnore))
                {
                    break;
                }

                directoryPath = Path.GetDirectoryName(directoryPath);
            }

            gitIgnoreMap[originalDirectoryPath] = new KeyValuePair<string, GitIgnore>(directoryPath, gitIgnore);
        }

        private bool GetGitIgnore(FileSystem fileSystem, ref string directoryPath, out GitIgnore gitIgnore)
        {
            KeyValuePair<string, GitIgnore> gitIgnoreEntry;
            gitIgnore = null;
            if (gitIgnoreMap.TryGetValue(directoryPath, out gitIgnoreEntry))
            {
                directoryPath = gitIgnoreEntry.Key;
                gitIgnore = gitIgnoreEntry.Value;
                return true;
            }

            foreach (var gitIgnoreFileName in gitIgnoreFileNames)
            {
                var gitIgnoreFilePath = Path.Combine(directoryPath, gitIgnoreFileName);
                if (File.Exists(gitIgnoreFilePath))
                {
                    try
                    {
                        Console.WriteLine("Parsing: " + gitIgnoreFilePath);
                        using (var gitIgnoreStream = fileSystem.OpenFile(gitIgnoreFilePath))
                        using (var reader = new StreamReader(gitIgnoreStream))
                        {
                            gitIgnore = GitIgnore.Parse(reader, tfIgnore: string.Equals(gitIgnoreFileName, ".tfignore", StringComparison.OrdinalIgnoreCase));
                            gitIgnoreMap[directoryPath] = new KeyValuePair<string, GitIgnore>(directoryPath, gitIgnore);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Error parsing: " + gitIgnoreFilePath);
                    }

                    return true;
                }
            }

            return false;
        }

        public override bool IncludeFile(FileSystem fileSystem, string filePath)
        {
            var directory = Path.GetDirectoryName(filePath);
            var gitIgnore = gitIgnoreMap.GetOrDefault(directory);

            return Include(filePath, gitIgnore.Key, gitIgnore.Value);
        }

        private bool Include(string filePath, string gitIgnoreDirectoryPath, GitIgnore gitIgnore, bool isDirectory = false)
        {
            var originalFilePath = filePath;

            // Exclude .git folder
            if (filePath.Contains(@"\.git\"))
            {
                return false;
            }

            if (gitIgnore == null)
            {
                return true;
            }

            int startIndex = gitIgnoreDirectoryPath.Length;

            while (filePath.Length > startIndex && (filePath[startIndex] == '\\' || filePath[startIndex] == '/'))
            {
                startIndex++;
            }

            filePath = filePath.Substring(startIndex);

            var include = !gitIgnore.Excludes(filePath);
            if (isDirectory && include)
            {
                include = !gitIgnore.Excludes(filePath + '/');
            }

            if (!include)
            {
                Console.WriteLine("Ignoring: " + originalFilePath);
            }

            return include;
        }
    }
}
