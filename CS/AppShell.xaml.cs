using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Maui.Controls;
using MvvmDemo.Modules;

namespace MvvmDemo;

public partial class AppShell : Shell {
    public AppShell() {
        InitializeComponent();
        Items.Add(new ShellContent() {
            Route = ModuleInfos.Home.Route,
            ContentTemplate = new DataTemplate(() => Ioc.Default.GetService(ModuleInfos.Home.ViewType))
        });
    }
}
