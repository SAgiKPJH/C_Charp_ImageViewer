using Application.ImageViewer.Abstractions;
using Application.ImageViewer.Models;
using System.IO;

namespace Infrastructure.Presentation.ImageViewer.Models;

public class PathImageSourceProvider : IImageSourceProvider
{
    private readonly string _directory;
    public PathImageSourceProvider(string directory) => _directory = directory;
    public Task<List<IImageModel>> GetImagesAsync()
    {
        var files = Directory.GetFiles(_directory, "*.jpg").Concat(Directory.GetFiles(_directory, "*.png"));
        var list = files.Select(f => new PathImageModel(f)).Cast<IImageModel>().ToList();
        return Task.FromResult(list);
    }
}