using ClientsManager.App.Helpers.Models;
using ClientsManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace ClientsManager.App.Helpers.EnumHelpers;

public static class DescriptionEnumHelper
{
    public static string Description(this Enum value)
    {
        var attributes = value?
            .GetType()?
            .GetField(value.ToString())?
            .GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attributes is not null && attributes.Any())
        {
            var desription = (attributes.First() as DescriptionAttribute)!.Description;
            return desription;
        }

        TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
        return textInfo.ToTitleCase(textInfo.ToLower(value?.ToString().Replace("_", " ") ?? ""));

    }

    public static IEnumerable<ValueDescription> GetAllValuesAndDescriptions(Type type)
    {
        if (!type.IsEnum)
        {
            throw new ArgumentException(null, nameof(type));
        }

        return Enum
            .GetValues(type)
            .Cast<Enum>()
            .Select(e => new ValueDescription() { Value = e, Description = e?.Description() });
    }
}
