using ClientsManager.App.Models;
using ClientsManager.App.ViewModels.Dialogs;
using ClientsManager.Application.Services.Interfaces;
using ClientsManager.Domain.Models;
using MaterialDesignThemes.Wpf;

namespace ClientsManager.App.ViewModels;

public class TableViewModel
{
    private const string AddDialogIdentifier = "Dialog";

    private readonly IOrdersService _ordersService;

    public TableViewModel(IOrdersService ordersService)
    {
        _ordersService = ordersService;
    }

    // TODO: Rename it
    public async void ShowAddOrderDialog()
    {
        var vm = new AddOrderDialogViewModel();
        var dialogResult = await DialogHost.Show(vm, AddDialogIdentifier);

        if (dialogResult is bool boolResult && boolResult)
        {
            var order = new OrderInfo()
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                AdditionalName = vm.AdditionalName,
                PhoneNumber = vm.PhoneNumber,
                CarModel = vm.CarModel,
                CarNumber = vm.CarNumber,
                Description = vm.Description,
                OrderType = vm.OrderType,
                Price = vm.Price
            };

            await _ordersService.AddAsync(order);
        }
    }

    // TODO: Rename it
    public async void ShowEditOrderDialog(OrderInfoModel orderInfo)
    {
        var vm = new EditOrderDialogViewModel()
        {
            FirstName = "Test" + orderInfo.Id
        };
        var dialogResult = await DialogHost.Show(vm, AddDialogIdentifier);

        if (dialogResult is bool boolResult && boolResult)
        {

        }
        else
        {

        }
    }

    public async void DeleteOrder(OrderInfoModel orderInfo)
    {
        var vm = new WarningMessageDialogViewModel()
        {
            Message = $"Вы собираетесь удалить запись под номером {orderInfo.Id}"
        };
        var dialogResult = await DialogHost.Show(vm, AddDialogIdentifier);

        if (dialogResult is bool boolResult && boolResult)
        {

        }
        else
        {

        }
    }
}
