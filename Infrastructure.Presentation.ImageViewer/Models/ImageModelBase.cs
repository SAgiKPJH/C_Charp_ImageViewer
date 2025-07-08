using System.ComponentModel;
using System.IO;
using System.Windows.Media.Imaging;

namespace Infrastructure.Presentation.ImageViewer.Models;

public abstract class ImageModelBase : IDisposable
{
    private readonly Lazy<Task<BitmapSource?>> _imageSourceLazy;
    public bool IsVisible { get; private set; }
    public BitmapSource DisplayImageSource { get; private set; } = null!;
    private static readonly BitmapSource? _noImage = System.Windows.Application.Current.Resources["NoImage"] as BitmapImage;
    private static MemoryStream _noImageStream => GetNoImageStream();

    protected ImageModelBase()
    {
        _imageSourceLazy = new Lazy<Task<BitmapSource?>>(LoadImageAsync, true);
    }

    protected abstract Task<MemoryStream?> GetImageAsync();

    public async Task<BitmapSource?> LoadImageAsync()
    {
        var stream = await GetImageAsync();
        return GetBitmap(stream);
    }

    public async Task VisibleAsync()
    {
        if (!IsVisible)
        {
            DisplayImageSource = await _imageSourceLazy.Value ?? GetPlaceholderImageSource();
            OnPropertyChanged(nameof(DisplayImageSource));
            IsVisible = true;
        }
    }

    private static BitmapSource? GetBitmap(MemoryStream? stream)
    {
        if (stream == null) return null;
        try
        {
            stream.Seek(0, SeekOrigin.Begin);
            var bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = stream;
            bmp.CacheOption = BitmapCacheOption.OnLoad;
            bmp.EndInit();
            bmp.Freeze();
            return bmp;
        }
        catch
        {
            return null;
        }
    }

    private static MemoryStream GetNoImageStream()
    {
        using var memoryStream = new MemoryStream();
        BitmapEncoder encoder = new PngBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(_noImage));
        encoder.Save(memoryStream);

        return new MemoryStream(memoryStream.ToArray());
    }

    protected MemoryStream GetPlaceholderImageStream() => _noImageStream;
    
    private BitmapSource GetPlaceholderImageSource()
    {
        var stream = GetPlaceholderImageStream();
        return GetBitmap(stream) ?? new BitmapImage();
    }

    public void Dispose() { /* 필요시 리소스 해제 */ }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}