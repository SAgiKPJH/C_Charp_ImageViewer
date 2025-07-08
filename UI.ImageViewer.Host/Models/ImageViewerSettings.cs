namespace UI.ImageViewer.Host.Models;

public class ImageViewerSettings
{
    public string DefaultImagePath { get; set; } = "C:\\Users\\jh.park\\Pictures\\Screenshots";
    public int ThumbnailPageSize { get; set; } = 6;
    public int MaxLoadedPages { get; set; } = 5;
    public bool ShowImageNames { get; set; } = false;
    public string WindowTitle { get; set; } = "Image Viewer";
    public int WindowWidth { get; set; } = 1000;
    public int WindowHeight { get; set; } = 800;
}