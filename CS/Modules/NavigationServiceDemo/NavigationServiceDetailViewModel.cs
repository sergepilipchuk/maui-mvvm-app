using Microsoft.Maui.Controls;

namespace MvvmDemo.Modules.NavigationServiceDemo;

public class NavigationServiceDetailViewModel : DXObservableObject, IQueryAttributable {
    public int NavigationParameter { get => navigationParameter; private set => SetProperty(ref navigationParameter, value); }
    public bool IsModal { get => isModal; private set => SetProperty(ref isModal, value); }
    public string? CurrentLocation { get => currentLocation; private set => SetProperty(ref currentLocation, value); }
    public int InstanceNumber { get; private set; }

    public AsyncRelayCommand GoBackCommand { get; }
    public RelayCommand OnNavigatedToCommand { get; }

    INavigationService NavigationService { get; }

    public NavigationServiceDetailViewModel(INavigationService navigationService) { 
        NavigationService = navigationService;
        GoBackCommand = new AsyncRelayCommand(GoBack);
        OnNavigatedToCommand = new RelayCommand(OnNavigatedTo);
        InstanceNumber = ++instanceNumber;
    }
    Task GoBack() {
        return NavigationService.PopAsync();
    }
    void OnNavigatedTo() {
        CurrentLocation = NavigationService.CurrentLocation;
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query) {
        NavigationParameter = (int)query[nameof(NavigationParameter)];
        IsModal = (bool)query[nameof(IsModal)];
    }
    
    int navigationParameter;
    bool isModal;
    string? currentLocation;
    static int instanceNumber;
}