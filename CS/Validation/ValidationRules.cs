namespace MvvmDemo.Validation;

public static class ValidationRules {
    public static IValidationRule<string?> IsNotNullOrWhiteSpace(string message) {
        return new ValidationRule<string?>(static x => !string.IsNullOrWhiteSpace(x), message);
    }
}
