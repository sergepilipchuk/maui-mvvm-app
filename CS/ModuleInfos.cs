using MvvmDemo.Modules.DispatcherDemo;
using MvvmDemo.Modules.PopupServiceDemo;
using MvvmDemo.Modules.FilePickerDemo;
using MvvmDemo.Modules.FileSystemDemo;
using MvvmDemo.Modules.HomeModule;
using MvvmDemo.Modules.LocalizationDemo;
using MvvmDemo.Modules.NavigationServiceDemo;
using MvvmDemo.Modules.PrintServiceDemo;
using MvvmDemo.Modules.SaveFilePickerDemo;
using MvvmDemo.Modules.UIServiceDemo;

namespace MvvmDemo;

public static class ModuleInfos {
    public static readonly ModuleInfo Home = new ModuleInfo("Home", "Home", typeof(HomePage), typeof(HomeViewModel), "");
    public static readonly ModuleInfo DispatcherDemo = new ModuleInfo("Dispatcher", "DispatcherDemo", typeof(DispatcherDemoPage), typeof(DispatcherDemoViewModel), "Using IDispatcher in ViewModel");
    public static readonly ModuleInfo NavigationServiceDemo = new ModuleInfo("Navigation Service", "NavigationServiceDemo", typeof(NavigationServiceDemoPage), typeof(NavigationServiceDemoViewModel), "Using INavigationService in ViewModel");
    public static readonly ModuleInfo PopupServiceDemo = new ModuleInfo("Popup Service", "PopupServiceDemo", typeof(PopupServiceDemoPage), typeof(PopupServiceDemoViewModel), "Using IDXPopupService in ViewModel");
    public static readonly ModuleInfo LocalizationServiceDemo = new ModuleInfo("Localization Service", "LocalizationDemo", typeof(LocalizationDemoPage), typeof(LocalizationDemoViewModel), "Using ILocalizationService in ViewModel");
    public static readonly ModuleInfo PrintServiceDemo = new ModuleInfo("Print Service", "PrintServiceDemo", typeof(PrintServiceDemoPage), typeof(PrintServiceDemoViewModel), "Using IPrintService in ViewModel");
    public static readonly ModuleInfo SaveFilePickerDemo = new ModuleInfo("Save File Picker", "SaveFilePickerDemo", typeof(SaveFilePickerDemoPage), typeof(SaveFilePickerDemoViewModel), "Using ISaveFilePicker in ViewModel");
    public static readonly ModuleInfo FilePickerDemo = new ModuleInfo("File Picker", "FilePickerDemo", typeof(FilePickerDemoPage), typeof(FilePickerDemoViewModel), "Using IFilePicker in ViewModel");
    public static readonly ModuleInfo FileSystemDemo = new ModuleInfo("File System", "FileSystemDemo", typeof(FileSystemDemoPage), typeof(FileSystemDemoViewModel), "Using IFileSystem in ViewModel");
    public static readonly ModuleInfo UIServiceDemo = new ModuleInfo("UI Service", "UIServiceDemo", typeof(UIServiceDemoPage), typeof(UIServiceDemoViewModel), "UI Services enable UI-aware functionality in a View Model");
    
    public static readonly ModuleInfo NavigationServiceDetailPage = new ModuleInfo("Detail Page", "NavigationServiceDetail", typeof(NavigationServiceDetailPage), typeof(NavigationServiceDetailViewModel), "");
    public static readonly PopupModuleInfo LoginPopup = new PopupModuleInfo(typeof(LoginPopup), typeof(LoginPopupViewModel));

    public static readonly ModuleInfo[] DemoModules = new[] {
        DispatcherDemo,
        NavigationServiceDemo,
        PopupServiceDemo,
        LocalizationServiceDemo,
        PrintServiceDemo,
        SaveFilePickerDemo,
        FilePickerDemo,
        FileSystemDemo,
        UIServiceDemo
    };
    public static readonly ModuleInfo[] Pages = new[] {
        NavigationServiceDetailPage
    };
    public static readonly ModuleInfo[] All = 
        Home.Yield()
        .Concat(Pages)
        .Concat(DemoModules)
        .ToArray();
    public static readonly PopupModuleInfo[] Popups = new[] {
        LoginPopup
    };

    public static ModuleInfo GetModuleInfoByViewType(Type viewType) {
        return All.First(x => x.ViewType == viewType);
    }
}

public class ModuleInfo {
    public string Title { get; }
    public string Route { get; }
    public Type ViewType { get; }
    public Type ViewModelType { get; }
    public string Description { get; }

    public ModuleInfo(string title, string route, Type viewType, Type viewModelType, string description) {
        Title = title;
        Route = route;
        ViewType = viewType;
        ViewModelType = viewModelType;
        Description = description;
    }
}
public class PopupModuleInfo {
    public Type ViewType { get; }
    public Type ViewModelType { get; }
    
    public PopupModuleInfo(Type viewType, Type viewModelType) {
        ViewType = viewType;
        ViewModelType = viewModelType;
    }
}