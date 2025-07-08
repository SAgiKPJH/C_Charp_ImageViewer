using Application.ImageViewer.Models;
using System.IO;

namespace Infrastructure.Presentation.ImageViewer.Models;

public class PathImageModel : ImageModelBase, IImageModel
{
    private readonly string _path;
    public string DisplayName => Path.GetFileName(_path);
    public PathImageModel(string path) => _path = path;
    protected override async Task<MemoryStream?> GetImageAsync()
    {
        if (!File.Exists(_path)) return null;
        var bytes = await File.ReadAllBytesAsync(_path);
        return new MemoryStream(bytes);
    }
    public override bool Equals(object obj) => obj is PathImageModel other && string.Equals(_path, other._path, System.StringComparison.OrdinalIgnoreCase);
    public override int GetHashCode() => _path.ToLowerInvariant().GetHashCode();
}