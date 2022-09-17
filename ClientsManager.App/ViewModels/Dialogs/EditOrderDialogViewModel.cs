using ClientsManager.App.ViewModels.Base;
using ClientsManager.Domain.Enums;
using System;

namespace ClientsManager.App.ViewModels.Dialogs;

public class EditOrderDialogViewModel : ViewModelBase
{
    public int Id { get; set; }

    #region Customer
    private string? _customer;

    public string? Customer
    {
        get => _customer;
        set
        {
            Set(ref _customer, value);
            CheckFormValidation();
        }
    }
    #endregion

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

    #region VIN
    private string? _vin;

    public string? VIN
    {
        get => _vin;
        set
        {
            Set(ref _vin, value);
            CheckFormValidation();
        }
    }
    #endregion

    #region Price
    private decimal? _price;

    public decimal? Price
    {
        get => _price;
        set
        {
            Set(ref _price, value);
            CheckFormValidation();
        }
    }
    #endregion

    #region ReleaseYear
    private int? _releaseYear;

    public int? ReleaseYear
    {
        get => _releaseYear;
        set
        {
            Set(ref _releaseYear, value);
            CheckFormValidation();
        }
    }
    #endregion

    public string? Description { get; set; }
    public OrderType OrderType { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

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
            !string.IsNullOrEmpty(Customer) &&
            !string.IsNullOrEmpty(PhoneNumber) &&
            !string.IsNullOrEmpty(CarModel) &&
            !string.IsNullOrEmpty(CarNumber) &&
            !string.IsNullOrEmpty(VIN) &&
            Price != 0 &&
            ReleaseYear is not null && ReleaseYear != 0;
    }
}
