using ClientsManager.App.ViewModels;
using ClientsManager.Application.Services.Interfaces;
using System.Threading.Tasks;

namespace ClientsManager.App.Commands.TableCommands;

public class NextPageAsyncCommand : TableAsyncCommandBase
{
    public NextPageAsyncCommand(TableViewModel tableViewModel, IOrdersService ordersService) 
        : base(tableViewModel, ordersService)
    { }

    public override async Task ExecuteAsync(object? parameter)
    {
        _tableViewModel.IsLoading = true;

        var currentPage = _tableViewModel.CurrentPageNumber;
        var pageSize = _tableViewModel.PageSize;
        var selectedTab = _tableViewModel.SelectedTab;

        _tableViewModel.Orders = await _ordersService.GetPageAsync(currentPage + 1, pageSize, selectedTab);
        _tableViewModel.CurrentPageNumber = currentPage + 1;

        _tableViewModel.IsLoading = false;
    }
}
