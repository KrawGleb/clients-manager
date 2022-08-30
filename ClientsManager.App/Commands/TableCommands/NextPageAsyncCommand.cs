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
        _tableViewModel.CurrentPageNumber += 1;
    }
}
