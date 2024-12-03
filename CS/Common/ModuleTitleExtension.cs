using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Xaml;

namespace MvvmDemo.Common;

[RequireService([ typeof(IProvideValueTarget) ])]
public class ModuleTitleExtension : IMarkupExtension {
    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) {
        var valueProvider = serviceProvider.GetRequiredService<IProvideValueTarget>();
        var viewType = valueProvider.TargetObject.GetType();
        var moduleInfo = ModuleInfos.GetModuleInfoByViewType(viewType);
        return moduleInfo.Title;
    }
}