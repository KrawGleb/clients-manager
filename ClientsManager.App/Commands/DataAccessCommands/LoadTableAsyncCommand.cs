using ClientsManager.App.ViewModels;
using ClientsManager.Application.Services.Interfaces;
using ClientsManager.Domain.Enums;
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

        if (parameter is null)
        {
            _tableViewModel.Orders = await _ordersService.GetByTypeAsync(_tableViewModel.SelectedTab);
        }
        else
        {
            var type = (OrderType)parameter;


            _tableViewModel.Orders = await _ordersService.GetByTypeAsync(type);
        }

        _tableViewModel.IsLoading = false;
    }
}
