namespace MvvmDemo.Modules.NavigationServiceDemo;

public class NavigationServiceDemoViewModel : DXObservableObject {
    public int NavigationParameter { get => navigationParameter; set => SetProperty(ref navigationParameter, value); }
    public bool ShowModal { get => showModal; set => SetProperty(ref showModal, value); }
    
    public string? CurrentLocation { get => currentLocation; private set => SetProperty(ref currentLocation, value); }
    public int InstanceNumber { get; private set; }
    
    public RelayCommand ShowDetailFormCommand { get; }
    public RelayCommand OnNavigatedToCommand { get; }
    INavigationService NavigationService { get; }

    public NavigationServiceDemoViewModel(INavigationService navigationService) {
        this.navigationParameter = 5;
        this.showModal = false;
        NavigationService = navigationService;
        ShowDetailFormCommand = new RelayCommand(ShowDetailForm);
        OnNavigatedToCommand = new RelayCommand(OnNavigatedTo);
        InstanceNumber = ++instanceNumber;
    }
    void ShowDetailForm() {
        var parameters = new Dictionary<string, object>();
        parameters["NavigationParameter"] = NavigationParameter;
        parameters["IsModal"] = ShowModal;
        NavigationService.GoToAsync(
            ModuleInfos.NavigationServiceDetailPage.Route, 
            parameters);
    }
    void OnNavigatedTo() {
        CurrentLocation = NavigationService.CurrentLocation;
    }

    int navigationParameter;
    bool showModal;
    string? currentLocation;
    static int instanceNumber;
}