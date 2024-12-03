using CommunityToolkit.Maui.Converters;
using Microsoft.Maui.Controls.Xaml;

namespace MvvmDemo.Common;

[AcceptEmptyServiceProvider]
public class InvertedBoolConverterExtension : InvertedBoolConverter { }

[AcceptEmptyServiceProvider]
public class BoolToObjectConverterExtension<T> : BoolToObjectConverter<T> { }

[AcceptEmptyServiceProvider]
public class IsStringNotNullOrEmptyConverterExtension : IsStringNotNullOrEmptyConverter { }