using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UI.ImageViewer.Components.Molecules;

public partial class PageNavigation : UserControl
{
    public static readonly DependencyProperty CurrentPageProperty =
        DependencyProperty.Register(nameof(CurrentPage), typeof(int), typeof(PageNavigation), new PropertyMetadata(1));

    public static readonly DependencyProperty TotalPagesProperty =
        DependencyProperty.Register(nameof(TotalPages), typeof(int), typeof(PageNavigation), new PropertyMetadata(1));

    public static readonly DependencyProperty FirstCommandProperty =
        DependencyProperty.Register(nameof(FirstCommand), typeof(ICommand), typeof(PageNavigation), new PropertyMetadata(null));

    public static readonly DependencyProperty PreviousCommandProperty =
        DependencyProperty.Register(nameof(PreviousCommand), typeof(ICommand), typeof(PageNavigation), new PropertyMetadata(null));

    public static readonly DependencyProperty NextCommandProperty =
        DependencyProperty.Register(nameof(NextCommand), typeof(ICommand), typeof(PageNavigation), new PropertyMetadata(null));

    public static readonly DependencyProperty LastCommandProperty =
        DependencyProperty.Register(nameof(LastCommand), typeof(ICommand), typeof(PageNavigation), new PropertyMetadata(null));

    public int CurrentPage
    {
        get { return (int)GetValue(CurrentPageProperty); }
        set { SetValue(CurrentPageProperty, value); }
    }

    public int TotalPages
    {
        get { return (int)GetValue(TotalPagesProperty); }
        set { SetValue(TotalPagesProperty, value); }
    }

    public ICommand FirstCommand
    {
        get { return (ICommand)GetValue(FirstCommandProperty); }
        set { SetValue(FirstCommandProperty, value); }
    }

    public ICommand PreviousCommand
    {
        get { return (ICommand)GetValue(PreviousCommandProperty); }
        set { SetValue(PreviousCommandProperty, value); }
    }

    public ICommand NextCommand
    {
        get { return (ICommand)GetValue(NextCommandProperty); }
        set { SetValue(NextCommandProperty, value); }
    }

    public ICommand LastCommand
    {
        get { return (ICommand)GetValue(LastCommandProperty); }
        set { SetValue(LastCommandProperty, value); }
    }

    public PageNavigation()
    {
        InitializeComponent();
    }
}