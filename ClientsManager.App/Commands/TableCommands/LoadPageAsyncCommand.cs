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
        if (parameter is null)
        {
            return;
        }

        _tableViewModel.IsLoading = true;

        var pageNumber = (int)parameter;
        var pageSize = _tableViewModel.PageSize;
        var selectedTab = _tableViewModel.SelectedTab;

        _tableViewModel.Orders = await _ordersService.GetPageAsync(pageNumber, pageSize, selectedTab);

        _tableViewModel.IsLoading = false;
    }
}
