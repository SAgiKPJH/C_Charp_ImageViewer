using Application.ImageViewer.Models;

namespace Application.ImageViewer.Abstractions;

public interface IImageSourceProvider
{
    Task<List<IImageModel>> GetImagesAsync();
}