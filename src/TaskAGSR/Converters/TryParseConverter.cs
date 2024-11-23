using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace TaskAGSR.Converters;

public abstract class TryParseConverter<T>(
    TryParseConverter<T>.TryParseDelegate tryParseDelegate) 
    : TypeConverter
{
    public delegate bool TryParseDelegate([NotNullWhen(true)] string? input, [NotNullWhen(true)] out T? output);

    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        => sourceType == typeof(string)
        || base.CanConvertFrom(context, sourceType);

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        => value is string input
        && tryParseDelegate(input, out T? output)
            ? output
            : base.ConvertFrom(context, culture, value);
}
