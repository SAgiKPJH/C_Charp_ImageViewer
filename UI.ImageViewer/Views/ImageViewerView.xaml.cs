using System.Windows;
using System.Windows.Controls;

namespace UI.ImageViewer.Views;

public partial class ImageViewerView : UserControl
{
    public string DisplayName
    {
        get { return (string)GetValue(DisplayNameProperty); }
        set { SetValue(DisplayNameProperty, value); }
    }

    public static readonly DependencyProperty DisplayNameProperty =
        DependencyProperty.Register(nameof(DisplayName), typeof(string), typeof(ImageViewerView), new PropertyMetadata(nameof(DisplayName)));

    public ImageViewerView()
    {
        InitializeComponent();
    }
} 