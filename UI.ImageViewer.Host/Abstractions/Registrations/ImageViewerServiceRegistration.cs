using Microsoft.Extensions.DependencyInjection;
using UI.ImageViewer.Host.Models;
using UI.ImageViewer.Host.Abstractions.Services;
using UI.ImageViewer.Host.Services;
using Infrastructure.Presentation.ImageViewer.Models;
using Infrastructure.Presentation.ImageViewer.ViewModels;
using Application.ImageViewer.Models;
using Application.ImageViewer.Abstractions;

namespace UI.ImageViewer.Host.Abstractions.Registrations;

public static class ImageViewerServiceRegistration
{
    public static IServiceCollection AddImageViewerServices(this IServiceCollection services, string imagePath)
    {
        // Settings
        services.AddSingleton<ImageViewerSettings>(sp => new ImageViewerSettings 
        { 
            DefaultImagePath = imagePath 
        });
        
        // Image Source Providers
        services.AddSingleton<IImageSourceProvider>(sp => new PathImageSourceProvider(imagePath));
        
        // ViewModels
        services.AddSingleton<ImageViewerViewModel>();
        
        // Models (if needed for DI)
        services.AddTransient<IImageModel, PathImageModel>();
        
        // Services
        services.AddSingleton<ILogService, ConsoleLogService>();
        services.AddSingleton<IMessageService, MessageBoxService>();
        
        return services;
    }
}