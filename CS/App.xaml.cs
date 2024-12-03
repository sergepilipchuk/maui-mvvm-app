using Microsoft.Maui.Controls;

namespace MvvmDemo;

public partial class App : Application {
    public App() {
        InitializeComponent();
    }
    protected override Window CreateWindow(Microsoft.Maui.IActivationState? activationState) {
        return new Window(new AppShell());
    }
}