using ClientsManager.App.ViewModels;
using ClientsManager.Application.Services.Interfaces;
using System.Threading.Tasks;

namespace ClientsManager.App.Commands.DataAccessCommands;

public class LoadTableAsyncCommand : AsyncCommandBase
{
    private readonly TableViewModel _tableViewModel;
    private readonly IOrdersService _ordersService;

    public LoadTableAsyncCommand(
        TableViewModel tableViewModel,
        IOrdersService ordersService)
    {
        _ordersService = ordersService;
        _tableViewModel = tableViewModel;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        _tableViewModel.IsLoading = true;
        
        // TODO: Remove this
        await Task.Delay(10000);
        _tableViewModel.Orders = await _ordersService.GetAllAsync();

        _tableViewModel.IsLoading = false;
    }
}
