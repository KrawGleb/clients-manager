using Caliburn.Micro;
using ClientsManager.App.Commands.DataAccessCommands;
using ClientsManager.App.Models;
using ClientsManager.App.ViewModels.Dialogs;
using ClientsManager.Application.Services.Interfaces;
using ClientsManager.Domain.Models;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Windows.Input;

namespace ClientsManager.App.ViewModels;

// TODO: Clean this
public class TableViewModel : Screen
{
    private const string DialogIdentifier = "Dialog";

    private readonly IOrdersService _ordersService;

    private bool _isLoading = false;
    public bool IsLoading
    {
        get => _isLoading;
        set => Set(ref _isLoading, value);
    }

    private IEnumerable<OrderInfo> _orders;
    public IEnumerable<OrderInfo> Orders
    {
        get => _orders;
        set => Set(ref _orders, value);
    }

    public ICommand LoadTableAsyncCommand { get; }
    public ICommand AddOrderAsyncCommand { get; }

    public TableViewModel(IOrdersService ordersService)
    {
        _ordersService = ordersService;

        LoadTableAsyncCommand = new LoadTableAsyncCommand(this, ordersService);
        AddOrderAsyncCommand = new AddOrderAsyncCommand(this, ordersService);
    }

    public static TableViewModel LoadTableViewModel(IOrdersService ordersService)
    {
        var tableViewModel = new TableViewModel(ordersService);

        tableViewModel.LoadTableAsyncCommand.Execute(null);

        return tableViewModel;
    }


    #region Old
    public async void EditOrder(OrderInfo orderInfo)
    {
        var command = new UpdateOrderAsyncCommand(this, _ordersService);

        await command.ExecuteAsync(orderInfo);
    }

    public async void DeleteOrder(OrderInfoModel orderInfo)
    {
        var vm = new WarningMessageDialogViewModel()
        {
            Message = $"Вы собираетесь удалить запись под номером {orderInfo.Id}"
        };
        var dialogResult = await DialogHost.Show(vm, DialogIdentifier);

        if (dialogResult is bool boolResult && boolResult)
        {

        }
        else
        {

        }
    } 
    #endregion
}
