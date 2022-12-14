using ClientsManager.App.ViewModels;
using ClientsManager.Application.Services.Interfaces;
using System;
using System.Windows.Input;

namespace ClientsManager.App.Commands.TableCommands;

public class GetTotalPagesCountCommand : ICommand
{
    private readonly TableViewModel _tableViewModel;
    private readonly IOrdersService _ordersService;

    public GetTotalPagesCountCommand(
        TableViewModel tableViewModel,
        IOrdersService ordersService)
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

        var parameters = _tableViewModel.GetItemsParameters();
        var totalCount = _ordersService.GetTotalCount(parameters.Tab, parameters.SearchOption, parameters.SearchValue);
        _tableViewModel.PaginationComponent.TotalPagesCount = (int)Math.Ceiling((double)totalCount / parameters.PageSize);

        _tableViewModel.IsLoading = false;

    }
}
