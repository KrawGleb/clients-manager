using ClientsManager.App.ViewModels;
using ClientsManager.App.ViewModels.Dialogs;
using ClientsManager.Application.Services.Interfaces;
using DocumentFormat.OpenXml.Drawing.Charts;
using MaterialDesignThemes.Wpf;
using System.Threading.Tasks;

namespace ClientsManager.App.Commands.DataAccessCommands;

public class ClearOrdersTableAsyncCommand : AsyncCommandBase
{
    private const string DialogIdentifier = "Dialog";

    private readonly TableViewModel _tableViewModel;
    private readonly IOrdersService _ordersService;

    public ClearOrdersTableAsyncCommand(
        TableViewModel tableViewModel,
        IOrdersService ordersService)
    {
        _ordersService = ordersService;
        _tableViewModel = tableViewModel;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        var vm = new WarningMessageDialogViewModel()
        {
            Message = "Вы собираетесь ОЧИСТИТЬ всю таблицу"
        };

        var dialogResult = await DialogHost.Show(vm, DialogIdentifier);

        if (dialogResult is bool boolResult && boolResult)
        {
            _tableViewModel.IsLoading = true;

            await _ordersService.ClearAsync();

            _tableViewModel.IsLoading = false;

            _tableViewModel.InitTableAsyncCommand.Execute(null);
        }
    }
}
