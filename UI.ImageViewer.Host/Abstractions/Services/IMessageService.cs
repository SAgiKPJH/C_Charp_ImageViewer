namespace UI.ImageViewer.Host.Abstractions.Services;

public interface IMessageService
{
    void ShowInformation(string message, string title = "Information");
    void ShowWarning(string message, string title = "Warning");
    void ShowError(string message, string title = "Error");
    bool ShowConfirmation(string message, string title = "Confirmation");
}