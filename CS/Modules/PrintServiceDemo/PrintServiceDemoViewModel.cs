namespace MvvmDemo.Modules.PrintServiceDemo;

public class PrintServiceDemoViewModel : DXObservableObject {
    public AsyncRelayCommand PrintCommand { get; }
    
    IPrintService PrintService { get; }

    public PrintServiceDemoViewModel(IPrintService printService) {
        PrintService = printService;
        PrintCommand = new AsyncRelayCommand(Print);
    }
    async Task Print() {
        await PrintService.PrintAsync("BalanceSheet.pdf");
    }
}