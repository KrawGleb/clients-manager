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
            FirstName = entry.FirstName,
            LastName = entry.LastName,
            AdditionalName = entry.AdditionalName,
            PhoneNumber = entry.PhoneNumber,
            Description = entry.Description,
            CarModel = entry.CarModel,
            CarNumber = entry.CarNumber,
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
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                AdditionalName = vm.AdditionalName,
                PhoneNumber = vm.PhoneNumber,
                Description = vm.Description,
                CarModel = vm.CarModel,
                CarNumber = vm.CarNumber,
                OrderType = vm.OrderType,
                Price = vm.Price
            };

            await _ordersService.UpdateAsync(newEntry);
            
            _tableViewModel.IsLoading = false;

            new InitTableAsyncCommand(_tableViewModel, _ordersService).Execute(null);
        }
    }
}
