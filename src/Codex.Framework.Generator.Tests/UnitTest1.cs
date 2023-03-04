using System.Runtime.CompilerServices;

namespace Codex.Framework.Generator.Tests;

public class TestClass
{
    [Fact]
    public void RunGenerator()
    {
        Program.Run(Path.Combine(Path.GetDirectoryName(ProjectPath), "generated"));
    }

    public static string ProjectPath { get; } = GetProjectPath();

    private static string GetProjectPath([CallerFilePath] string filePath = null)
    {
        return Path.GetDirectoryName(filePath);
    }
}