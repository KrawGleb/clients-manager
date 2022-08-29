using ClientsManager.App.ViewModels;
using ClientsManager.Application.Services.Interfaces;

namespace ClientsManager.App.Commands.TableCommands;

public abstract class TableAsyncCommandBase : AsyncCommandBase
{
    protected readonly TableViewModel _tableViewModel;
    protected readonly IOrdersService _ordersService;

    public TableAsyncCommandBase(
        TableViewModel tableViewModel,
        IOrdersService ordersService)
    {
        _ordersService = ordersService;
        _tableViewModel = tableViewModel;
    }
}
