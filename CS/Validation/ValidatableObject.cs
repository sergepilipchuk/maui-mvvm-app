namespace MvvmDemo.Validation;

public interface IValidationRule<T> {
    string? Validate(T? value);
}
public class ValidationRule<T> : IValidationRule<T> {
    readonly Func<T?, bool> check;
    readonly string message;

    public ValidationRule(Func<T?, bool> check, string message) {
        this.check = check;
        this.message = message;
    }
    string? IValidationRule<T>.Validate(T? value) {
        return check(value) ? null : message;
    }
}

public class ValidatableObject<T> : DXObservableObject {
    public T? Value { get => value; set => SetProperty(ref this.value, value); }
    public string? Error { get => error; private set => SetProperty(ref error, value); }
    public bool IsValid { get => isValid; private set => SetProperty(ref isValid, value, OnIsValidChanged); }

    public IRelayCommand ValidateCommand { get; }
    public IRelayCommand ClearErrorCommand { get; }

    public event EventHandler? IsValidChanged;

    public ValidatableObject(IValidationRule<T> rule, Action? onIsValidChanged = null) 
        : this(default, rule, onIsValidChanged) { }
    public ValidatableObject(T? initValue, IValidationRule<T> rule, Action? onIsValidChanged = null) {
        this.value = initValue;
        this.rule = rule;
        this.onIsValidChanged = onIsValidChanged;
        this.isValid = true;
        ValidateCommand = new RelayCommand(() => Validate());
        ClearErrorCommand = new RelayCommand(ClearError);
    }
    
    public bool Validate() {
        Error = rule.Validate(Value);
        IsValid = string.IsNullOrEmpty(Error);
        return IsValid;
    }
    public void ClearError() {
        Error = null;
        IsValid = true;
    }
    
    void OnIsValidChanged() {
        onIsValidChanged?.Invoke();
        IsValidChanged?.Invoke(this, EventArgs.Empty);
    }

    T? value;
    string? error;
    bool isValid;
    readonly Action? onIsValidChanged;
    readonly IValidationRule<T> rule;
}