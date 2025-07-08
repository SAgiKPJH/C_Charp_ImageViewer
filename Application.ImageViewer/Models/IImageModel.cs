using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Application.ImageViewer.Models;

public interface IImageModel : INotifyPropertyChanged
{
    string DisplayName { get; }
    BitmapSource DisplayImageSource { get; }
    Task VisibleAsync();
    void Dispose();
}