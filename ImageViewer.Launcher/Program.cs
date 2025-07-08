using UI.ImageViewer.Host;
using System.IO;
using System.Windows;

namespace ImageViewer.Launcher;

public class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        string path = GetPathFromArguments(args);
        
        var app = new App(path);
        
        AddResourceDictionary(app, "/UI.ImageViewer.Host;component/Abstractions/Resources/Themes/Light/ColorResource.xaml");
        AddResourceDictionary(app, "/UI.ImageViewer.Host;component/Abstractions/Resources/Themes/Light/ImageResource.xaml");
        AddResourceDictionary(app, "/UI.ImageViewer.Host;component/Abstractions/Resources/ImageTransform.xaml");
        
        app.Run();
    }

    private static void AddResourceDictionary(App app, string source)
    {
        try
        {
            var resourceDict = new ResourceDictionary
            {
                Source = new Uri(source, UriKind.Relative)
            };
            app.Resources.MergedDictionaries.Add(resourceDict);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Warning: Failed to load resource dictionary {source}: {ex.Message}");
        }
    }

    private static string GetPathFromArguments(string[] args)
    {
        string defaultPath = "C:\\Users\\juhyu\\Pictures\\Screenshots";
        
        if (args.Length == 0)
        {
            Console.WriteLine($"No path provided, using default path: {defaultPath}");
            return defaultPath;
        }

        string providedPath = args[0].Trim('"');
        
        if (Directory.Exists(providedPath))
        {
            Console.WriteLine($"Using provided path: {providedPath}");
            return providedPath;
        }

        Console.WriteLine($"Warning: Provided path '{providedPath}' does not exist.");
        Console.WriteLine($"Using default path: {defaultPath}");
        return defaultPath;
    }
}