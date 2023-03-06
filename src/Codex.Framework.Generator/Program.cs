using System.Runtime.CompilerServices;
using Xunit;

namespace Codex.Framework.Generator;
public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    private static void Run(string outputPath)
    {
        Directory.CreateDirectory(outputPath);
        var context = new GeneratorContext(outputPath);
        context.Initialize();
    }

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
