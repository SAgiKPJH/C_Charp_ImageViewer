using System;

namespace UI.ImageViewer.Host.Abstractions.Services;

public interface ILogService
{
    void LogInformation(string message);
    void LogWarning(string message);
    void LogError(string message);
    void LogError(string message, Exception exception);
}