using System.Globalization;
using System.Windows.Controls;

namespace ClientsManager.App.Helpers.ValidationRules;

public class RequiredValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        string? valueAsString = value as string;

        if (string.IsNullOrEmpty(valueAsString))
            return new ValidationResult(false, "Это обязательное поле");

        return new ValidationResult(true, null);
    }
}
