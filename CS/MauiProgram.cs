using System.Resources;
using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.DependencyInjection;
using DevExpress.Maui;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using MvvmDemo.Modules.PopupServiceDemo;

namespace MvvmDemo;

public static class MauiProgram {
    public static MauiApp CreateMauiApp() {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseDevExpress(
                dxLocalizationResources: new ResourceManager("MvvmDemo.Resources.Strings.DevExpressMaui", typeof(MauiProgram).Assembly),
                appLocalizationResources: new ResourceManager("MvvmDemo.Resources.Strings.AppResources", typeof(MauiProgram).Assembly))
            .UseDevExpressControls()
            .UseDevExpressEditors()
            .UseDevExpressCollectionView()
            .ConfigureFonts(fonts => {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .RegisterServices()
            .RegisterViewsAndViewModels();
        var res = builder.Build();
        Ioc.Default.ConfigureServices(res.Services);
        return res;
    }
    static MauiAppBuilder RegisterViewsAndViewModels(this MauiAppBuilder builder) {
        ModuleInfos.All.ForEach(x => {
            builder.Services
                .AddTransient(x.ViewType)
                .AddTransient(x.ViewModelType);
            if(x != ModuleInfos.Home)
                Routing.RegisterRoute(x.Route, x.ViewType);
        });
        ModuleInfos.Popups.ForEach(x => {
            builder.Services
                .AddTransientDXPopup(x.ViewType, x.ViewModelType);
        });
        return builder;
    }
    static MauiAppBuilder RegisterServices(this MauiAppBuilder builder) {
        builder.Services
            .AddSingleton<Microsoft.Maui.Storage.IFileSystem>(
                x => Microsoft.Maui.Storage.FileSystem.Current)
            .AddSingleton<Microsoft.Maui.Storage.IFilePicker>(
                x => Microsoft.Maui.Storage.FilePicker.Default)
            .AddSingleton<ILoginService>(x => new LoginService());
        return builder;
    }
}