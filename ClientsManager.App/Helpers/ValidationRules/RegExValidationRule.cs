using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace ClientsManager.App.Helpers.ValidationRules;

public class RegExValidationRule : ValidationRule
{
    public string RegExPattern { get; set; }
    public string Example { get; set; }

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        var valueAsString = value as string;

        if (valueAsString is not null && !Regex.IsMatch(valueAsString, RegExPattern))
        {
            return new ValidationResult(false, $"Это поле должно быть в формате {Example}");
        }

        return new ValidationResult(true, null);
    }
}
