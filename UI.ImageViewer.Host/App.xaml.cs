using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using UI.ImageViewer.Host.Abstractions;
using UI.ImageViewer.Host.Abstractions.Registrations;
using UI.ImageViewer.Host.Abstractions.Services;
using UI.ImageViewer.Host.Abstractions.MarkupExtensions;
using UI.ImageViewer.Host.Models;

namespace UI.ImageViewer.Host;

public partial class App : System.Windows.Application
{
    public string Path { get; private set; } = "C:\\Users\\jh.park\\Pictures\\Screenshots";
    public IServiceProvider ServiceProvider { get; private set; } = default!;

    public App() { }
    
    public App(string path)
    {
        Path = path;
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        try
        {
            InitializeServices();
            ConfigureDependencyInjection();
            ShowMainWindow();
            LogApplicationStart();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Application startup failed: {ex.Message}", "Startup Error", 
                          MessageBoxButton.OK, MessageBoxImage.Error);
            Shutdown();
        }
    }

    private void InitializeServices()
    {
        var services = new ServiceCollection();
        
        // 기본 서비스 등록
        services.AddImageViewerServices(Path);
        
        // MainWindow 팩토리 등록
        services.AddSingleton<MainWindow>(CreateMainWindow);
        
        ServiceProvider = services.BuildServiceProvider();
    }

    private void ConfigureDependencyInjection()
    {
        // ServiceLocator 설정
        ServiceLocator.ConfigureServices(ServiceProvider);
        
        // DependencySource 설정
        DependencySource.Resolver = Resolve;
    }

    private MainWindow CreateMainWindow(IServiceProvider sp)
    {
        var settings = sp.GetRequiredService<ImageViewerSettings>();
        var logService = sp.GetRequiredService<ILogService>();
        
        var window = new MainWindow();
        ConfigureWindowProperties(window, settings);
        
        logService.LogInformation("MainWindow created and configured");
        return window;
    }

    private void ConfigureWindowProperties(MainWindow window, ImageViewerSettings settings)
    {
        window.Title = settings.WindowTitle;
        window.Width = settings.WindowWidth;
        window.Height = settings.WindowHeight;
    }

    private void ShowMainWindow()
    {
        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        MainWindow = mainWindow;
        mainWindow.Show();
    }

    private void LogApplicationStart()
    {
        var logService = ServiceProvider.GetRequiredService<ILogService>();
        logService.LogInformation("Application started successfully");
    }

    private object Resolve(Type type, object key, string name) => 
        ServiceProvider.GetService(type) ?? default!;
}