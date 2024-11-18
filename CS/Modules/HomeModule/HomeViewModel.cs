namespace MvvmDemo.Modules.HomeModule;

public class HomeViewModel : DXObservableObject {
    public ModuleInfo[] Demos { get => ModuleInfos.DemoModules; }
    public RelayCommand<ModuleInfo> ShowDemoCommand { get; }
    INavigationService NavigationService { get; }

    public HomeViewModel(INavigationService navigationService) {
        NavigationService = navigationService;
        ShowDemoCommand = new RelayCommand<ModuleInfo>(ShowDemo);
    }
    void ShowDemo(ModuleInfo? item) {
        ArgumentNullException.ThrowIfNull(item);
        NavigationService.GoToAsync(item.Route);
    }
}