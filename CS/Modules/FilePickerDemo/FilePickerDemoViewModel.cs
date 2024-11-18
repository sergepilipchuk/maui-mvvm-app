using Microsoft.Maui.Storage;

namespace MvvmDemo.Modules.FilePickerDemo;

public class FilePickerDemoViewModel : DXObservableObject {
    public string? PickedFile { get => pickedFile; private set => SetProperty(ref pickedFile, value); }

    public AsyncRelayCommand OpenCommand { get; }
    
    IFilePicker FilePicker { get; }
    
    public FilePickerDemoViewModel(IFilePicker filePicker) { 
        FilePicker = filePicker;
        OpenCommand = new AsyncRelayCommand(Open);
    }
    async Task Open() {
        var res = await FilePicker.PickAsync();
        PickedFile = res?.FileName;
    }

    string? pickedFile;
}