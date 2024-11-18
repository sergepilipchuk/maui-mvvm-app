using Microsoft.Maui.Controls;

namespace MvvmDemo;

public partial class App : Application {
    public App() {
        InitializeComponent();
        MainPage = new AppShell();
    }
}