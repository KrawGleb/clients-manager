using ClientsManager.App.ViewModels;
using ClientsManager.Domain.Enums;
using System;
using System.Windows.Input;

namespace ClientsManager.App.Commands.TableCommands;

public class ChangeTabCommand : ICommand
{
    private readonly TableViewModel _tableViewModel;

    public ChangeTabCommand(TableViewModel tableViewModel)
    {
        _tableViewModel = tableViewModel;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
        => true;

    public void Execute(object? parameter)
    {
        if (parameter is null)
        {
            return;
        }

        OrderType selectedTab = (OrderType)parameter;

        if (_tableViewModel.SelectedTab == selectedTab)
        {
            return;
        }

        _tableViewModel.SelectedTab = selectedTab;

        _tableViewModel.InitTableAsyncCommand.Execute(null);
    }
}
