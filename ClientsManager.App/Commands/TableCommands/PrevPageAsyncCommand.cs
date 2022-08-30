using ClientsManager.App.ViewModels;
using ClientsManager.Application.Services.Interfaces;
using System.Threading.Tasks;

namespace ClientsManager.App.Commands.TableCommands;

public class PrevPageAsyncCommand : TableAsyncCommandBase
{
    public PrevPageAsyncCommand(TableViewModel tableViewModel, IOrdersService ordersService)
        : base(tableViewModel, ordersService)
    { }

    public override async Task ExecuteAsync(object? parameter)
    {
        _tableViewModel.PaginationComponent.CurrentPageNumber -= 1;
    }
}
