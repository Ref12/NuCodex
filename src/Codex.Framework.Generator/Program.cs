namespace Codex.Framework.Generator;
public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    public static void Run(string outputPath)
    {
        Directory.CreateDirectory(outputPath);
        var context = new GeneratorContext(outputPath);
        context.Initialize();
    }
}
