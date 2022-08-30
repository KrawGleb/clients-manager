using ClientsManager.App.ViewModels;
using ClientsManager.Application.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace ClientsManager.App.Commands.TableCommands;

public class LoadPageAsyncCommand : TableAsyncCommandBase
{
    public LoadPageAsyncCommand(TableViewModel tableViewModel, IOrdersService ordersService) 
        : base(tableViewModel, ordersService)
    { }

    public override async Task ExecuteAsync(object? parameter)
    {
        _tableViewModel.IsLoading = true;

        var tableItemsParams = _tableViewModel.GetItemsParameters();

        _tableViewModel.Orders = await _ordersService.GetPageAsync(
            tableItemsParams.PageNumber,
            tableItemsParams.PageSize,
            tableItemsParams.Tab,
            tableItemsParams.SearchOption,
            tableItemsParams.SearchValue);


        new GetTotalPagesCountCommand(_tableViewModel, _ordersService).Execute(null);

        _tableViewModel.IsLoading = false;
    }
}
