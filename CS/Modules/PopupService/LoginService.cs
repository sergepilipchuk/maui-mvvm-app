namespace MvvmDemo.Modules.PopupServiceDemo;

public interface ILoginService {
    Task Login(string? login, string? password);
}
public class LoginService : ILoginService {
    async Task ILoginService.Login(string? login, string? password) {
        await Task.Delay(2500);
    }
}