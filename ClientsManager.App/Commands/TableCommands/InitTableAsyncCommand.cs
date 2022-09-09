using ClientsManager.App.ViewModels;
using ClientsManager.Application.Services.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClientsManager.App.Commands.TableCommands;

public class InitTableCommand : ICommand
{
    private readonly TableViewModel _tableViewModel;
    private readonly IOrdersService _ordersService;

    public InitTableCommand(TableViewModel tableViewModel, IOrdersService ordersService) 
    {
        _tableViewModel = tableViewModel;
        _ordersService = ordersService;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
        => true;

    public void Execute(object? parameter)
    {
        _tableViewModel.IsLoading = true;

        new GetTotalPagesCountCommand(_tableViewModel, _ordersService).Execute(null);

        _tableViewModel.PaginationComponent.CurrentPageNumber = 1;

        _tableViewModel.IsLoading = false;
    }
}
