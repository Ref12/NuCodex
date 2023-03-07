namespace Codex.Utilities
{
    public class FileSystem : IDisposable
    {
        public FileSystem()
        {
        }

        public virtual IEnumerable<string> GetFiles()
        {
            return new string[0];
        }

        public virtual IEnumerable<string> GetFiles(string relativeDirectoryPath)
        {
            return new string[0];
        }

        public virtual Stream OpenFile(string filePath)
        {
            return Stream.Null;
        }

        public virtual long GetFileSize(string filePath)
        {
            return 0;
        }

        public virtual void Dispose()
        {
        }
    }

    public class SystemFileSystem : FileSystem
    {
        public override Stream OpenFile(string filePath)
        {
            return File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
        }

        public override long GetFileSize(string filePath)
        {
            return new FileInfo(filePath).Length;
        }
    }
}