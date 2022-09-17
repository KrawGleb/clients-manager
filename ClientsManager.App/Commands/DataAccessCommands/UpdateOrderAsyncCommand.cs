using ClientsManager.App.Commands.TableCommands;
using ClientsManager.App.ViewModels;
using ClientsManager.App.ViewModels.Dialogs;
using ClientsManager.Application.Services.Interfaces;
using ClientsManager.Domain.Models;
using MaterialDesignThemes.Wpf;
using System;
using System.Threading.Tasks;

namespace ClientsManager.App.Commands.DataAccessCommands;

public class UpdateOrderAsyncCommand : AsyncCommandBase
{
    private const string DialogIdentifier = "Dialog";

    private readonly TableViewModel _tableViewModel;
    private readonly IOrdersService _ordersService;

    public UpdateOrderAsyncCommand(
        TableViewModel tableViewModel,
        IOrdersService ordersService)
    {
        _tableViewModel = tableViewModel;
        _ordersService = ordersService;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        var entry = parameter as OrderInfo;

        ArgumentNullException.ThrowIfNull(entry);

        var vm = new EditOrderDialogViewModel()
        {
            Id = entry.Id,
            Customer = entry.Customer,
            PhoneNumber = entry.PhoneNumber,
            Description = entry.Description,
            CarModel = entry.CarModel,
            CarNumber = entry.CarNumber,
            ReleaseYear = entry.CarReleaseYear,
            VIN = entry.VIN,
            CreatedDate = entry.CreatedDate,
            OrderType = entry.OrderType,
            Price = entry.Price
        };
        var dialogResult = await DialogHost.Show(vm, DialogIdentifier);

        if (dialogResult is bool boolResult && boolResult)
        {
            _tableViewModel.IsLoading = true;

            var newEntry = new OrderInfo()
            {
                Id = vm.Id,
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

            await _ordersService.UpdateAsync(newEntry);
            
            _tableViewModel.IsLoading = false;

            _tableViewModel.InitTableAsyncCommand.Execute(null);
        }
    }
}
