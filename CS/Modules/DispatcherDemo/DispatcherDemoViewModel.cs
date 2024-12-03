using Microsoft.Maui.Dispatching;

namespace MvvmDemo.Modules.DispatcherDemo;

public class DispatcherDemoViewModel : DXObservableObject {
    public ObservableCollection<string> Items { get; }
    public AsyncRelayCommand GenerateItemsCommand { get; }

    IDispatcher Dispatcher { get; }
    public DispatcherDemoViewModel(IDispatcher dispatcher) {
        Dispatcher = dispatcher;
        Items = new();
        GenerateItemsCommand = new AsyncRelayCommand(Generate);
    }

    Task Generate() {
        return Task.Factory.StartNew(GenerateCore);
    }
    void GenerateCore() {
        Dispatcher.DispatchAsync(() => Items.Clear());
        for (int i = 0; i <= 20; i++) {
            string item = "Item " + i;
            Dispatcher.DispatchAsync(() => Items.Add(item));
            Thread.Sleep(TimeSpan.FromMilliseconds(100));
        }
    }
}