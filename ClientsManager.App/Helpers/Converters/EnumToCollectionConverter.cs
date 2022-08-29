using ClientsManager.App.Helpers.EnumHelpers;
using ClientsManager.App.Helpers.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace ClientsManager.App.Helpers.Converters;

[ValueConversion(typeof(Enum), typeof(IEnumerable<ValueDescription>))]
public class EnumToCollectionConverter : MarkupExtension, IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
        DescriptionEnumHelper.GetAllValuesAndDescriptions(value.GetType());

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => null;

    public override object ProvideValue(IServiceProvider serviceProvider)
        => this;
}
