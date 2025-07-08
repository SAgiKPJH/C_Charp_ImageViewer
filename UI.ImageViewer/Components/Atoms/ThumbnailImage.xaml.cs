using System.Windows;
using System.Windows.Controls;

namespace UI.ImageViewer.Components.Atoms;

public partial class ThumbnailImage : UserControl
{
    public static readonly DependencyProperty ShowImageNamesProperty =
        DependencyProperty.Register(nameof(ShowImageNames), typeof(bool), typeof(ThumbnailImage), new PropertyMetadata(true));

    public bool ShowImageNames
    {
        get { return (bool)GetValue(ShowImageNamesProperty); }
        set { SetValue(ShowImageNamesProperty, value); }
    }

    public ThumbnailImage()
    {
        InitializeComponent();
    }
}