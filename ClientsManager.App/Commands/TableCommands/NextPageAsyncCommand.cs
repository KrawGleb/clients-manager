using ClientsManager.App.ViewModels;
using ClientsManager.Application.Services.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClientsManager.App.Commands.TableCommands;

public class NextPageAsyncCommand : ICommand
{
    private readonly TableViewModel _tableViewModel;

    public NextPageAsyncCommand(TableViewModel tableViewModel) 
    {
        _tableViewModel = tableViewModel;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
        => true;

    public void Execute(object? parameter)
    {
        _tableViewModel.PaginationComponent.CurrentPageNumber += 1;
    }
}
