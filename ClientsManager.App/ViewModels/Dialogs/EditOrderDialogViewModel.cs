using ClientsManager.App.ViewModels.Base;
using ClientsManager.Domain.Enums;

namespace ClientsManager.App.ViewModels.Dialogs;

public class EditOrderDialogViewModel : ViewModelBase
{
    public int Id { get; set; }

    #region FirstName
    private string? _firstName;

    public string? FirstName
    {
        get => _firstName;
        set
        {
            Set(ref _firstName, value);
            CheckFormValidation();
        }
    }
    #endregion

    #region LastName
    private string? _lastName;

    public string? LastName
    {
        get => _lastName;
        set
        {
            Set(ref _lastName, value);
            CheckFormValidation();
        }
    }
    #endregion

    public string? AdditionalName { get; set; }

    #region PhoneNumber
    private string? _phoneNumber;

    public string? PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            Set(ref _phoneNumber, value);
            CheckFormValidation();
        }
    }
    #endregion

    #region CarModel
    private string? _carModel;

    public string? CarModel
    {
        get => _carModel;
        set
        {
            Set(ref _carModel, value);
            CheckFormValidation();
        }
    }
    #endregion

    #region CarNumber
    private string? _carNumber;

    public string? CarNumber
    {
        get => _carNumber;
        set
        {
            Set(ref _carNumber, value);
            CheckFormValidation();
        }
    }
    #endregion

    #region Price
    private decimal _price;

    public decimal Price
    {
        get => _price;
        set
        {
            Set(ref _price, value);
            CheckFormValidation();
        }
    }
    #endregion


    public string? Description { get; set; }
    public OrderType OrderType { get; set; }

    #region IsFormValid
    private bool _isFormValid;

    public bool IsFormValid
    {
        get => _isFormValid;
        set => Set(ref _isFormValid, value);
    }
    #endregion

    private void CheckFormValidation()
    {
        IsFormValid =
            !string.IsNullOrEmpty(FirstName) &&
            !string.IsNullOrEmpty(LastName) &&
            !string.IsNullOrEmpty(PhoneNumber) &&
            !string.IsNullOrEmpty(CarModel) &&
            !string.IsNullOrEmpty(CarNumber) &&
            Price != 0;
    }
}
