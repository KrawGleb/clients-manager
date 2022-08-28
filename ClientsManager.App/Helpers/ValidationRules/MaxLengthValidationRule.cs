using System.Globalization;
using System.Windows.Controls;

namespace ClientsManager.App.Helpers.ValidationRules;

public class MaxLengthValidationRule : ValidationRule
{
    public int MaxLength { get; set; }

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        var valueAsString = value as string;

        if (valueAsString is not null && valueAsString.Length > MaxLength)
        {
            return new ValidationResult(false, $"Длина этого поля не может превышать {MaxLength} символов");
        }

        return new ValidationResult(true, null);
    }
}
