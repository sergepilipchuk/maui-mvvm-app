using System.Text;

namespace MvvmDemo.Modules.SaveFilePickerDemo;

public class SaveFilePickerDemoViewModel : DXObservableObject {
    public AsyncRelayCommand SaveCommand { get; }
    
    ISaveFilePicker SaveFilePicker { get; }
    
    public SaveFilePickerDemoViewModel(ISaveFilePicker saveFilePicker) { 
        SaveFilePicker = saveFilePicker;
        SaveCommand = new AsyncRelayCommand(Save);
    }
    async Task Save() {
        var text = "Hello world!";
        using(var s = new MemoryStream(Encoding.UTF8.GetBytes(text))) {
            await SaveFilePicker.SaveAsync(s, "HelloWorld.txt", PredefinedFileType.Any);
        }
    }
}