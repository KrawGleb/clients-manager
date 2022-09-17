using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace ClientsManager.App.Helpers.Converters;

public class StringToPhoneConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
            return string.Empty;

        var phoneNo = value
            .ToString()!
            .Replace("(", string.Empty)
            .Replace(")", string.Empty)
            .Replace(" ", string.Empty)
            .Replace("+375", string.Empty)
            .Replace("+", string.Empty)
            .Replace("-", string.Empty);

        return phoneNo.Length switch
        {
            9 => Regex.Replace(phoneNo, @"(\d{2})(\d{3})(\d{2})(\d{2})", "+375 ($1) $2-$3-$4"),
            12 => Regex.Replace(phoneNo, @"(\d{3})(\d{2})(\d{3})(\d{2})(\d{2})", "+$1 ($2) $3-$4-$5"),
            _ => phoneNo,
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
}
