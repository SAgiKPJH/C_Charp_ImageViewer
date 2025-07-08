using System;
using Microsoft.Extensions.DependencyInjection;

namespace UI.ImageViewer.Host.Abstractions;

public static class ServiceLocator
{
    public static IServiceProvider ServiceProvider { get; private set; } = default!;

    public static void ConfigureServices(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }
}