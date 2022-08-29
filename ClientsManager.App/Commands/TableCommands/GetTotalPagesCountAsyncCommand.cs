using ClientsManager.App.ViewModels;
using ClientsManager.Application.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace ClientsManager.App.Commands.TableCommands;

public class GetTotalPagesCountAsyncCommand : TableAsyncCommandBase
{
    public GetTotalPagesCountAsyncCommand(TableViewModel tableViewModel, IOrdersService ordersService) 
        : base(tableViewModel, ordersService)
    { }

    public async override Task ExecuteAsync(object? parameter)
    {
        _tableViewModel.IsLoading = true;

        var selectedTab = _tableViewModel.SelectedTab;
        var pageSize = _tableViewModel.PageSize;
        var totalCount = await _ordersService.GetTotalCountAsync(selectedTab);
        _tableViewModel.TotalPagesCount = (int)Math.Ceiling((double)totalCount / pageSize);

        _tableViewModel.IsLoading = false;
    }
}
