namespace MvvmDemo.Modules.PopupServiceDemo;

public class PopupServiceDemoViewModel : DXObservableObject {
    public bool AllowScrim { get => allowScrim; set => SetProperty(ref allowScrim, value); }
    public bool CloseOnScrimTap { get => closeOnScrimTap; set => SetProperty(ref closeOnScrimTap, value); }
    public string? Result { get => result; private set => SetProperty(ref result, value); }
    public ObservableCollection<AlertCustomContentItem> AlertCustomContentItems { get; }

    public AsyncRelayCommand ShowAlertWithIconCommand { get; }
    public AsyncRelayCommand ShowAlertWithoutIconCommand { get; }
    public AsyncRelayCommand ShowAlertWithCustomContentCommand { get; }
    public AsyncRelayCommand ShowActionSheetWithIconsCommand { get; }
    public AsyncRelayCommand ShowActionSheetWithoutIconsCommand { get; }
    public AsyncRelayCommand ShowActionSheetWithoutCancelCommand { get; }
    public AsyncRelayCommand ShowOptionSheetWithRadioButtonsCommand { get; }
    public AsyncRelayCommand ShowOptionSheetWithCheckBoxesCommand { get; }
    public AsyncRelayCommand ShowCustomPopupCommand { get; }

    public PopupServiceDemoViewModel(IDXPopupService popupService) {
        this.popupService = popupService;
        ShowAlertWithIconCommand = new(ShowAlertWithIcon);
        ShowAlertWithoutIconCommand = new(ShowAlertWithoutIcon);
        ShowAlertWithCustomContentCommand = new(ShowAlertWithCustomContent);
        ShowActionSheetWithIconsCommand = new(ShowActionSheetWithIcons);
        ShowActionSheetWithoutIconsCommand = new(ShowActionSheetWithoutIcons);
        ShowActionSheetWithoutCancelCommand = new(ShowActionSheetWithoutCancel);
        ShowOptionSheetWithRadioButtonsCommand = new(ShowOptionSheetWithRadioButtons);
        ShowOptionSheetWithCheckBoxesCommand = new(ShowOptionSheetWithCheckBoxes);
        ShowCustomPopupCommand = new(ShowCustomPopup);

        AlertCustomContentItems = new() {
            new AlertCustomContentItem("Cindy_Haneline@example.com", "photo1"),
            new AlertCustomContentItem("Bruce_Cambell@example.com", "photo2")
        };
    }

    Task ShowAlertWithIcon() {
        return ShowAlertCore(true, null);
    }
    Task ShowAlertWithoutIcon() {
        return ShowAlertCore(false, null);
    }
    Task ShowAlertWithCustomContent() {
        var message = "This action will reset your app preferences back to their default settings. The following accounts will also be signed out:";
        return ShowAlertCore(false, "DXPopupAlert.CustomContent.Style", message);
    }
    async Task ShowAlertCore(bool showIcon, string? styleKey, string? message = null) {
        var title = "Reset Settings?";
        message ??= "This action will reset your app preferences back to their default settings.";
        var icon = showIcon ? "restart" : null;

        var res = await popupService.ShowAlert(
            settings: new DXPopupSettings() {
                Title = title,
                Message = message,
                TitleIcon = icon,
                VerticalAlignment = DXPopupVerticalAlignment.Center,
                AllowScrim = AllowScrim,
                CloseOnScrimTap = CloseOnScrimTap,
                StyleKey = styleKey,
                BindingContext = this
            },
            ok: "Accept",
            cancel: "Cancel");
        Result = res ? "True" : "False";
    }

    Task ShowActionSheetWithIcons() {
        return ShowActionSheetCore(true);
    }
    Task ShowActionSheetWithoutIcons() {
        return ShowActionSheetCore(false);
    }
    Task ShowActionSheetWithoutCancel() {
        return ShowActionSheetCore(false, false);
    }
    async Task ShowActionSheetCore(bool showIcons, bool showCancel = true) {
        var res = await popupService.ShowActionSheet(
            settings: new DXPopupSettings() {
                Title = "Actions",
                VerticalAlignment = DXPopupVerticalAlignment.Center,
                AllowScrim = AllowScrim,
                CloseOnScrimTap = CloseOnScrimTap,
            },
            cancel: 
                showCancel ? "Cancel" : null,
            actionButtons: new[] {
                new DXPopupActionInfo(text: "Copy", icon: showIcons ? "copy" : null),
                new DXPopupActionInfo(text: "Download", icon: showIcons ? "download" : null),
                new DXPopupActionInfo(text: "Share", icon: showIcons ? "share" : null)
            }
        );
        Result = res ?? "NULL";
    }

    async Task ShowOptionSheetWithRadioButtons() {
        var res = await popupService.ShowRadioOptionSheet(
            settings: new DXPopupSettings() {
                Title = "Phone Ringtone",
                AllowScrim = AllowScrim,
                CloseOnScrimTap = CloseOnScrimTap,
            },
            ok: "Accept",
            cancel: "Cancel",
            optionButtons: new[] {
                new DXPopupOptionInfo(text: "Default"),
                new DXPopupOptionInfo(text: "None"),
                new DXPopupOptionInfo(text: "Andromeda", isChecked: true),
                new DXPopupOptionInfo(text: "Aquila"),
                new DXPopupOptionInfo(text: "Backroad"),
                new DXPopupOptionInfo(text: "Bell phone"),
                new DXPopupOptionInfo(text: "Callisto"),
                new DXPopupOptionInfo(text: "Ganymede"),
                new DXPopupOptionInfo(text: "Luna"),
                new DXPopupOptionInfo(text: "Oberon"),
                new DXPopupOptionInfo(text: "Phobos"),
                new DXPopupOptionInfo(text: "Titania"),
                new DXPopupOptionInfo(text: "Triton"),
                new DXPopupOptionInfo(text: "Umbriel"),
            }
        );
        Result = res ?? "NULL";
    }
    async Task ShowOptionSheetWithCheckBoxes() {
        var res = await popupService.ShowCheckBoxOptionSheet(
            settings: new DXPopupSettings() {
                Title = "Label As",
                AllowScrim = AllowScrim,
                CloseOnScrimTap = CloseOnScrimTap,
            },
            ok: "Accept",
            cancel: "Cancel",
            optionButtons: new[] {
                new DXPopupOptionInfo(text: "None"),
                new DXPopupOptionInfo(text: "Forums"),
                new DXPopupOptionInfo(text: "Social", isChecked: true),
                new DXPopupOptionInfo(text: "Updates", isChecked: true),
                new DXPopupOptionInfo(text: "Promotions"),
                new DXPopupOptionInfo(text: "Spam"),
                new DXPopupOptionInfo(text: "Work"),
            }
        );
        if(res == null) {
            Result = $"Result: NULL";
            return;
        }
        Result = string.Join(", ", res);
    }

    async Task ShowCustomPopup() {
        var vm = await popupService.ShowPopup<LoginPopupViewModel>(x => {
            x.AllowScrim = AllowScrim;
            x.CloseOnScrimTap = CloseOnScrimTap;
        });
        if (!vm.Result) {
            Result = "NULL";
            return;
        }
        Result = $"Login={vm.Login.Value}, Password={vm.Password.Value}";
    }

    readonly IDXPopupService popupService;
    string? result = "NULL";
    bool allowScrim = true;
    bool closeOnScrimTap;
}

public class AlertCustomContentItem {
    public string Text { get; }
    public string Photo { get; }

    public AlertCustomContentItem(string text, string photo) {
        Text = text;
        Photo = photo;
    }
}
