using MvvmDemo.Validation;

namespace MvvmDemo.Modules.PopupServiceDemo;

public class LoginPopupViewModel : DXObservableObject, IDXPopupViewModel {
    public bool AllowScrim { get => allowScrim; set => SetProperty(ref allowScrim, value); }
    public bool CloseOnScrimTap { get => closeOnScrimTap; set => SetProperty(ref closeOnScrimTap, value); }

    public ValidatableObject<string?> Login { get; }
    public ValidatableObject<string?> Password { get; }
    public bool IsBusy { get => isBusy; private set => SetProperty(ref isBusy, value); }
    
    public bool Result { get; private set; }
    public AsyncRelayCommand LoginCommand { get; }
    
    public LoginPopupViewModel(ILoginService loginService) {
        this.loginService = loginService;
        LoginCommand = new(DoLogin, CanLogin);
        Login = new(
            ValidationRules.IsNotNullOrWhiteSpace("A login is required."), 
            () => LoginCommand.NotifyCanExecuteChanged());
        Password = new(
            ValidationRules.IsNotNullOrWhiteSpace("A password is required."),
            () => LoginCommand.NotifyCanExecuteChanged());
    }
    async Task DoLogin() {
        Login.Validate();
        Password.Validate();
        if(!Login.IsValid || !Password.IsValid)
            return;
        await loginService.Login(Login.Value, Password.Value);
        Result = true;
        popup!.Close();
    }
    bool CanLogin() {
        return Login.IsValid && Password.IsValid;
    }

    IDXPopup IDXPopupViewModel.Popup { set => popup = value; }

    readonly ILoginService loginService;
    IDXPopup? popup;
    bool allowScrim;
    bool closeOnScrimTap;
    bool isBusy;
}