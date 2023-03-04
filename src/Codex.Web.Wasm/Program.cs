using System;
using System.Runtime.InteropServices.JavaScript;
using System.Threading;
using System.Threading.Tasks;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine($"Hello4, Browser! from: {Thread.CurrentThread.ManagedThreadId}");

        for (int i = 0; i < 10; i++)
        {
            Task.Run(async () =>
            {
                Thread.Sleep(1000);
                Console.WriteLine($"Running thread with id: {Thread.CurrentThread.ManagedThreadId} on '{MyClass.GetHRef()}'");
                await Task.Delay(1000);
            });
        }
    }
}

public partial class MyClass
{
    [JSExport]
    internal static string Greeting()
    {
        var text = $"Hello, World2! Greetings from {GetHRef()}";
        Console.WriteLine(text);
        return text;
    }

    [JSImport("window.location.href", "main.js")]
    internal static partial string GetHRef();
}
