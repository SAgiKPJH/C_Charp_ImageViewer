using System.Windows;
using System.Windows.Media;

namespace UI.ImageViewer.Components.Atoms;

public partial class NavigationButton : System.Windows.Controls.Button
{
    public ImageSource Glyph
    {
        get { return (ImageSource)GetValue(GlyphProperty); }
        set { SetValue(GlyphProperty, value); }
    }

    public static readonly DependencyProperty GlyphProperty =
        DependencyProperty.Register(nameof(Glyph), typeof(ImageSource),
            typeof(NavigationButton));

    public NavigationButton()
    {
        InitializeComponent();
    }
}