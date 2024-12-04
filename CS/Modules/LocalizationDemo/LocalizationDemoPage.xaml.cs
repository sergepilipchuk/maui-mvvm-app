using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;

namespace MvvmDemo.Modules.LocalizationDemo;

public partial class LocalizationDemoPage : ContentPage {
    public LocalizationDemoPage() {
        InitializeComponent();
    }
    async void OnLearnMoreClicked(Object sender, EventArgs e) {
        var url = "https://github.com/dotnet/maui/issues/26338";
        var uri = new Uri(url, UriKind.Absolute);
        await Launcher.Default.OpenAsync(uri);
    }
}