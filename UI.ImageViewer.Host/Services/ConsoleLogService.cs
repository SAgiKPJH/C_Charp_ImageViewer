using System;
using UI.ImageViewer.Host.Abstractions.Services;

namespace UI.ImageViewer.Host.Services;

public class ConsoleLogService : ILogService
{
    public void LogInformation(string message)
    {
        Console.WriteLine($"[INFO] {DateTime.Now:yyyy-MM-dd HH:mm:ss}: {message}");
    }

    public void LogWarning(string message)
    {
        Console.WriteLine($"[WARN] {DateTime.Now:yyyy-MM-dd HH:mm:ss}: {message}");
    }

    public void LogError(string message)
    {
        Console.WriteLine($"[ERROR] {DateTime.Now:yyyy-MM-dd HH:mm:ss}: {message}");
    }

    public void LogError(string message, Exception exception)
    {
        Console.WriteLine($"[ERROR] {DateTime.Now:yyyy-MM-dd HH:mm:ss}: {message}");
        Console.WriteLine($"Exception: {exception}");
    }
}