using ClientsManager.App.ViewModels;
using ClientsManager.Application.Services.Interfaces;
using System.Threading.Tasks;

namespace ClientsManager.App.Commands.TableCommands;

public class SearchOrdersAsyncCommand : TableAsyncCommandBase
{
    public SearchOrdersAsyncCommand(TableViewModel tableViewModel, IOrdersService ordersService) 
        : base(tableViewModel, ordersService)
    { }

    public override async Task ExecuteAsync(object? parameter)
    {
        await (new LoadPageAsyncCommand(_tableViewModel, _ordersService).ExecuteAsync(null));
        _tableViewModel.PaginationComponent.CurrentPageNumber = 1;
    }
}
