using Microsoft.Maui.Storage;

namespace MvvmDemo.Modules.FileSystemDemo;

public class FileSystemDemoViewModel : DXObservableObject {
    public string? AppDataDirectory { get; private set; }
    public string? CacheDirectory { get; private set; }

    IFileSystem FileSystem { get; }
    
    public FileSystemDemoViewModel(IFileSystem fileSystem) { 
        FileSystem = fileSystem;
        AppDataDirectory = FileSystem.AppDataDirectory;
        CacheDirectory = FileSystem.CacheDirectory;
    }
}