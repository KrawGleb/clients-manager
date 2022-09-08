using ClientsManager.App.ViewModels;
using ClientsManager.Application.Services.Interfaces;
using System.Threading.Tasks;

namespace ClientsManager.App.Commands.TableCommands;

public class SearchAsyncCommand : TableAsyncCommandBase
{
    public SearchAsyncCommand(TableViewModel tableViewModel, IOrdersService ordersService) 
        : base(tableViewModel, ordersService)
    { }

    public override async Task ExecuteAsync(object? parameter)
    {
        _tableViewModel.IsLoading = true;

        var parameters = _tableViewModel.GetItemsParameters();

        _tableViewModel.IsLoading = false;
    }
}
