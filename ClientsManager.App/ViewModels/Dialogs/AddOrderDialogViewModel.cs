using ClientsManager.App.ViewModels.Base;
using ClientsManager.Domain.Enums;

namespace ClientsManager.App.ViewModels.Dialogs;

public class AddOrderDialogViewModel : ViewModelBase
{
    private string _firstName;

    public string FirstName
    {
        get => _firstName;
        set
        {
            Set(ref _firstName, value);
            CheckFormValidation();
        }
    }

    private string _lastName;

    public string LastName
    {
        get => _lastName; 
        set
        {
            Set(ref _lastName, value);
            CheckFormValidation();
        }
    }

    public string? AdditionalName { get; set; }

    private string _phoneNumber;

    public string PhoneNumber
    {
        get => _phoneNumber; 
        set
        {
            Set(ref _phoneNumber, value);
            CheckFormValidation();
        }
    }

    public string? CarModel { get; set; }
    public string? CarNumber { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public OrderType OrderType { get; set; }

    private bool _isFormValid;

    public bool IsFormValid
    {
        get => _isFormValid;
        set => Set(ref _isFormValid, value);
    }

    private void CheckFormValidation()
    {
        IsFormValid =
            !string.IsNullOrEmpty(FirstName) &&
            !string.IsNullOrEmpty(LastName) &&
            !string.IsNullOrEmpty(PhoneNumber);
    }
}
