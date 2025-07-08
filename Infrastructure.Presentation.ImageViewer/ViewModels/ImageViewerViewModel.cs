using System.Collections.ObjectModel;
using System.ComponentModel;
using Application.ImageViewer.Models;
using Application.ImageViewer.Abstractions;
using DevExpress.Mvvm.CodeGenerators;
using System.Windows.Input;

namespace Infrastructure.Presentation.ImageViewer.ViewModels;

[GenerateViewModel]
public partial class ImageViewerViewModel
{
    private const int PageSize = 6; // 한 페이지 썸네일 수
    private const int MaxLoadedPages = 5;

    private List<IImageModel> allImageModels = new(); // 전체 이미지 모델
    private Dictionary<int, List<IImageModel>> loadedPages = new(); // 페이지별 로딩된 모델
    private int currentThumbnailPage = 0;

    [GenerateProperty]
    ObservableCollection<IImageModel> visibleImages = new();

    [GenerateProperty]
    IImageModel selectedImage;

    [GenerateProperty]
    bool showImageNames = false;

    public int TotalCount => allImageModels.Count;
    public int SelectedIndex => SelectedImage != null ? allImageModels.IndexOf(SelectedImage) + 1 : 0;
    public int TotalThumbnailPages => (allImageModels.Count + PageSize - 1) / PageSize;
    public int CurrentThumbnailPage
    {
        get => currentThumbnailPage + 1;
        set { if (value - 1 != currentThumbnailPage) { currentThumbnailPage = value - 1; LoadPage(currentThumbnailPage, SelectedImage); OnPropertyChanged(); } }
    }

    public ImageViewerViewModel(IImageSourceProvider provider)
    {
        LoadAllImageModels(provider);
    }

    private async void LoadAllImageModels(IImageSourceProvider provider)
    {
        var imgs = await provider.GetImagesAsync();
        allImageModels = imgs.ToList();
        OnPropertyChanged(nameof(TotalThumbnailPages));
        MoveToPage(0, null);
    }

    private void MoveToPage(int page, IImageModel imageToSelect = null)
    {
        if (page < 0 || page >= TotalThumbnailPages) return;
        LoadPage(page, imageToSelect);
        currentThumbnailPage = page;
        OnPropertyChanged(nameof(CurrentThumbnailPage));
        OnPropertyChanged(nameof(TotalThumbnailPages));
    }

    private void LoadPage(int page, IImageModel imageToSelect = null)
    {
        VisibleImages.Clear();
        if (!loadedPages.ContainsKey(page))
        {
            int start = page * PageSize;
            var models = allImageModels.Skip(start).Take(PageSize).ToList();
            loadedPages[page] = models;
        }
        foreach (var model in loadedPages[page])
        {
            model.VisibleAsync();
            VisibleImages.Add(model);
        }
        // 5페이지 초과 시 가장 오래된 페이지 모델 Dispose 및 제거
        while (loadedPages.Count > MaxLoadedPages)
        {
            var oldest = loadedPages.Keys.OrderBy(k => k).First();
            foreach (var model in loadedPages[oldest])
                model.Dispose();
            loadedPages.Remove(oldest);
        }
        // 페이지 이동 시, imageToSelect가 있으면 그 이미지, 없으면 첫 번째 이미지
        if (imageToSelect != null && VisibleImages.Contains(imageToSelect))
            SelectedImage = imageToSelect;
        else
            SelectedImage = VisibleImages.FirstOrDefault();
    }

    [GenerateCommand]
    void FirstThumbnailPage()
    {
        if (currentThumbnailPage > 0)
            MoveToPage(0, SelectedImage);
    }
    bool CanFirstThumbnailPage() => currentThumbnailPage > 0;

    [GenerateCommand]
    void PrevThumbnailPage()
    {
        if (currentThumbnailPage > 0)
            MoveToPage(currentThumbnailPage - 1, SelectedImage);
    }
    bool CanPrevThumbnailPage() => currentThumbnailPage > 0;

    [GenerateCommand]
    void NextThumbnailPage()
    {
        if (currentThumbnailPage < TotalThumbnailPages - 1)
            MoveToPage(currentThumbnailPage + 1, SelectedImage);
    }
    bool CanNextThumbnailPage() => currentThumbnailPage < TotalThumbnailPages - 1;

    [GenerateCommand]
    void LastThumbnailPage()
    {
        if (currentThumbnailPage < TotalThumbnailPages - 1)
            MoveToPage(TotalThumbnailPages - 1, SelectedImage);
    }
    bool CanLastThumbnailPage() => currentThumbnailPage < TotalThumbnailPages - 1;

    [GenerateCommand]
    void PrevImage()
    {
        var currentIndex = allImageModels.IndexOf(SelectedImage);
        if (currentIndex > 0)
        {
            var prevImage = allImageModels[currentIndex - 1];
            SelectedImage = prevImage;
            EnsureSelectedImageVisible(prevImage);
        }
    }
    bool CanPrevImage() => SelectedImage != null && allImageModels.IndexOf(SelectedImage) > 0;

    [GenerateCommand]
    void NextImage()
    {
        var currentIndex = allImageModels.IndexOf(SelectedImage);
        if (currentIndex < allImageModels.Count - 1)
        {
            var nextImage = allImageModels[currentIndex + 1];
            SelectedImage = nextImage;
            EnsureSelectedImageVisible(nextImage);
        }
    }
    bool CanNextImage() => SelectedImage != null && allImageModels.IndexOf(SelectedImage) < allImageModels.Count - 1;

    [GenerateCommand]
    void FirstImage()
    {
        if (allImageModels.Count > 0)
        {
            var first = allImageModels[0];
            SelectedImage = first;
            EnsureSelectedImageVisible(first);
        }
    }
    bool CanFirstImage() => allImageModels.Count > 0 && SelectedImage != allImageModels.FirstOrDefault();

    [GenerateCommand]
    void LastImage()
    {
        if (allImageModels.Count > 0)
        {
            var last = allImageModels[allImageModels.Count - 1];
            SelectedImage = last;
            EnsureSelectedImageVisible(last);
        }
    }
    bool CanLastImage() => allImageModels.Count > 0 && SelectedImage != allImageModels.LastOrDefault();

    private void EnsureSelectedImageVisible(IImageModel imageToSelect)
    {
        if (imageToSelect == null) return;
        var selectedIndex = allImageModels.IndexOf(imageToSelect);
        var requiredPage = (selectedIndex / PageSize);
        if (requiredPage != currentThumbnailPage)
        {
            MoveToPage(requiredPage, imageToSelect);
        }
    }

    // SelectedImage가 바뀔 때마다 Lazy 로딩 트리거 및 SelectedIndex 갱신
    void OnSelectedImageChanged(IImageModel value)
    {
        if (value != null)
            _ = value.VisibleAsync();
        OnPropertyChanged(nameof(SelectedImage));
        OnPropertyChanged(nameof(SelectedIndex));
    }



    [GenerateCommand]
    void KeyDown(System.Windows.Input.KeyEventArgs e)
    {
        if (e == null) return;
        if (e.Key == Key.Left && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
        {
            if (PrevThumbnailPageCommand.CanExecute(null))
                PrevThumbnailPageCommand.Execute(null);
            e.Handled = true;
        }
        else if (e.Key == Key.Right && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
        {
            if (NextThumbnailPageCommand.CanExecute(null))
                NextThumbnailPageCommand.Execute(null);
            e.Handled = true;
        }
        else if (e.Key == Key.Left)
        {
            if (PrevImageCommand.CanExecute(null))
                PrevImageCommand.Execute(null);
            e.Handled = true;
        }
        else if (e.Key == Key.Right)
        {
            if (NextImageCommand.CanExecute(null))
                NextImageCommand.Execute(null);
            e.Handled = true;
        }
        else if (e.Key == Key.PageUp)
        {
            if (PrevThumbnailPageCommand.CanExecute(null))
                PrevThumbnailPageCommand.Execute(null);
            e.Handled = true;
        }
        else if (e.Key == Key.PageDown)
        {
            if (NextThumbnailPageCommand.CanExecute(null))
                NextThumbnailPageCommand.Execute(null);
            e.Handled = true;
        }
        else if (e.Key == Key.Home)
        {
            if (FirstImageCommand.CanExecute(null))
                FirstImageCommand.Execute(null);
            e.Handled = true;
        }
        else if (e.Key == Key.End)
        {
            if (LastImageCommand.CanExecute(null))
                LastImageCommand.Execute(null);
            e.Handled = true;
        }
    }

    [GenerateCommand]
    void MouseWheel(MouseWheelEventArgs e)
    {
        if (e == null) return;
        
        if (e.Delta > 0) // 휠을 위로 스크롤 - 이전 이미지
        {
            if (PrevImageCommand.CanExecute(null))
                PrevImageCommand.Execute(null);
        }
        else // 휠을 아래로 스크롤 - 다음 이미지
        {
            if (NextImageCommand.CanExecute(null))
                NextImageCommand.Execute(null);
        }
        
        e.Handled = true;
    }

    protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}