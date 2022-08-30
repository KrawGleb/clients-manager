using ClientsManager.App.ViewModels;
using ClientsManager.Application.Services.Interfaces;
using System.Threading.Tasks;

namespace ClientsManager.App.Commands.TableCommands;

public class InitTableAsyncCommand : TableAsyncCommandBase
{
    public InitTableAsyncCommand(TableViewModel tableViewModel, IOrdersService ordersService) 
        : base(tableViewModel, ordersService)
    { }

    public override async Task ExecuteAsync(object? parameter)
    {
        _tableViewModel.IsLoading = true;

        _tableViewModel.LoadPageAsyncCommand.Execute(
            _tableViewModel.PaginationComponent.CurrentPageNumber);

        _tableViewModel.IsLoading = false;
    }
}
