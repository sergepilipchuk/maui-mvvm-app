using System.Globalization;

namespace MvvmDemo.Modules.LocalizationDemo;

public class LocalizationDemoViewModel : DXObservableObject {
    public LocalizableString SimpleString { get; }
    public LocalizableString ParameterizedString { get; }
    public string? Parameter { get => parameter; set => SetProperty(ref parameter, value, OnParameterChanged); }
    
    public RelayCommand ChangeLanguageCommand { get; }
    ILocalizer Localizer { get; }

    public LocalizationDemoViewModel(ILocalizer localizer) {
        this.parameter = "John";
        Localizer = localizer;
        SimpleString = new LocalizableString(StringId.SimpleString);
        ParameterizedString = new LocalizableString(
            StringId.ParameterizedString,
            x => string.Format(x, Parameter));
        ChangeLanguageCommand = new RelayCommand(ChangeLanguage);
    }
    void OnParameterChanged() {
        ParameterizedString.Update();
    }
    void ChangeLanguage() {
        var culture = CultureInfo.CurrentCulture == fr ? en : fr;
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;
        Localizer.NotifyCultureChanged();
    }

    string? parameter;
    static readonly CultureInfo en = new CultureInfo("en-us");
    static readonly CultureInfo fr = new CultureInfo("fr-FR");
}