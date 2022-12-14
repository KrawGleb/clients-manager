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

        _tableViewModel.Orders.Clear();
        var collection = await _ordersService.GetPageAsync(
            tableItemsParams.PageNumber,
            tableItemsParams.PageSize,
            tableItemsParams.Tab,
            tableItemsParams.SearchOption,
            tableItemsParams.SearchValue,
            tableItemsParams.SortBy,
            tableItemsParams.SortOrder);
        _tableViewModel.Orders.AddRange(collection);

        new GetTotalPagesCountCommand(_tableViewModel, _ordersService).Execute(null);
        
        _tableViewModel.IsLoading = false;
    }
}
