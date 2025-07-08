using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UI.ImageViewer.Components.Molecules;

public partial class ThumbnailListBox : UserControl
{
    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), typeof(ThumbnailListBox), new PropertyMetadata(null));

    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(ThumbnailListBox), new PropertyMetadata(null));

    public static readonly DependencyProperty MouseWheelCommandProperty =
        DependencyProperty.Register(nameof(MouseWheelCommand), typeof(ICommand), typeof(ThumbnailListBox), new PropertyMetadata(null));

    public static readonly DependencyProperty ShowImageNamesProperty =
        DependencyProperty.Register(nameof(ShowImageNames), typeof(bool), typeof(ThumbnailListBox), new PropertyMetadata(true));

    public IEnumerable ItemsSource
    {
        get { return (IEnumerable)GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }

    public object SelectedItem
    {
        get { return GetValue(SelectedItemProperty); }
        set { SetValue(SelectedItemProperty, value); }
    }

    public ICommand MouseWheelCommand
    {
        get { return (ICommand)GetValue(MouseWheelCommandProperty); }
        set { SetValue(MouseWheelCommandProperty, value); }
    }

    public bool ShowImageNames
    {
        get { return (bool)GetValue(ShowImageNamesProperty); }
        set { SetValue(ShowImageNamesProperty, value); }
    }

    public ThumbnailListBox()
    {
        InitializeComponent();
    }
}