using Caliburn.Micro;
using ClientsManager.Domain.Enums;

namespace ClientsManager.App.ViewModels.Dialogs;

public class AddOrderDialogViewModel
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? AdditionalName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? CarModel { get; set; }
    public string? CarNumber { get; set; }
    public string? Price { get; set; }
    public string? Description { get; set; }
    public OrderType OrderType { get; set; }
}
