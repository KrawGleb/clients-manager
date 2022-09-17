using ClientsManager.App.ViewModels;
using ClientsManager.App.ViewModels.Dialogs;
using ClientsManager.Application.Services.Interfaces;
using ClientsManager.Domain.Models;
using MaterialDesignThemes.Wpf;
using System.Threading.Tasks;

namespace ClientsManager.App.Commands.DataAccessCommands;

public class AddOrderAsyncCommand : AsyncCommandBase
{
    private const string DialogIdentifier = "Dialog";

    private readonly TableViewModel _tableViewModel;
    private readonly IOrdersService _ordersService;

    public AddOrderAsyncCommand(
        TableViewModel tableViewModel,
        IOrdersService orderService)
    {
        _tableViewModel = tableViewModel;
        _ordersService = orderService;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        var vm = new AddOrderDialogViewModel();
        var dialogResult = await DialogHost.Show(vm, DialogIdentifier);

        if (dialogResult is bool boolResult && boolResult)
        {
            // ToDo: Use Automapper here
            var order = new OrderInfo()
            {
                Customer = vm.Customer,
                PhoneNumber = vm.PhoneNumber,
                CarModel = vm.CarModel,
                CarNumber = vm.CarNumber,
                Description = vm.Description,
                OrderType = vm.OrderType,
                Price = vm.Price ?? 0,
                CreatedDate = vm.CreatedDate,
                VIN = vm.VIN,
                CarReleaseYear = vm.ReleaseYear ?? 0,
            };

            _tableViewModel.IsLoading = true;

            await _ordersService.AddAsync(order);

            _tableViewModel.IsLoading = false;

            _tableViewModel.InitTableAsyncCommand.Execute(null);
        }
    }
}
