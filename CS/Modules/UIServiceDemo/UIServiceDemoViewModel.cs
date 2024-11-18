using DevExpress.Maui.Core;

namespace MvvmDemo.Modules.UIServiceDemo;

public class UIServiceDemoViewModel : DXObservableObject, IUIServiceClient {
    public List<string> Items { get; }
    public RelayCommand ScrollToStartCommand { get; }
    public RelayCommand ScrollToEndCommand { get; }

    IUIServiceContainer ServiceContainer { get; } = new UIServiceContainer();
    IUIServiceContainer IUIServiceClient.ServiceContainer { get => ServiceContainer; }

    public UIServiceDemoViewModel() {
        var items = new List<string>();
        for (int i = 0; i <= 20; i++) {
            items.Add($"Item {i}");
        }
        Items = items;
        ScrollToStartCommand = new RelayCommand(ScrollToStart);
        ScrollToEndCommand = new RelayCommand(ScrollToEnd);
    }
 
    void ScrollToStart() {
        var collectionView = ServiceContainer.GetRequiredService<IUIObjectService>();
        collectionView.Object.ScrollTo(0, DXScrollToPosition.Start);
    }
    void ScrollToEnd() {
        var collectionView = ServiceContainer.GetRequiredService<ICollectionViewUIService>();
        collectionView.ScrollToEnd();
    }
}