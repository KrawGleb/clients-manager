using ClientsManager.App.Commands.TableCommands;
using ClientsManager.App.ViewModels;
using ClientsManager.App.ViewModels.Dialogs;
using ClientsManager.Application.Services;
using ClientsManager.Application.Services.Interfaces;
using ClientsManager.Domain.Models;
using DocumentFormat.OpenXml.Drawing.Charts;
using MaterialDesignThemes.Wpf;
using System;
using System.Threading.Tasks;

namespace ClientsManager.App.Commands.DataAccessCommands;

public class DeleteOrderAsyncCommand : AsyncCommandBase
{
    private const string DialogIdentifier = "Dialog";

    private readonly TableViewModel _tableViewModel;
    private readonly IOrdersService _ordersService;

    public DeleteOrderAsyncCommand(
        TableViewModel tableViewModel,
        IOrdersService ordersService)
    {
        _ordersService = ordersService;
        _tableViewModel = tableViewModel;
    }

    public async override Task ExecuteAsync(object? parameter)
    {
        if (parameter is null)
        {
            return;
        }

        var orderId = (int)parameter;

        var vm = new WarningMessageDialogViewModel()
        {
            Message = $"Вы собираетесь удалить запись под номером {orderId}"
        };
        var dialogResult = await DialogHost.Show(vm, DialogIdentifier);

        if (dialogResult is bool boolResult && boolResult)
        {
            _tableViewModel.IsLoading = true;

            await _ordersService.DeleteAsync(orderId);

            _tableViewModel.IsLoading = false;

            _tableViewModel.InitTableAsyncCommand.Execute(null);
        }
    }
}
