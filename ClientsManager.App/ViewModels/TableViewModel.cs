using Caliburn.Micro;
using ClientsManager.App.Commands.DataAccessCommands;
using ClientsManager.App.ViewModels.Dialogs;
using ClientsManager.Application.Services.Interfaces;
using ClientsManager.Domain.Enums;
using ClientsManager.Domain.Models;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Windows.Input;

namespace ClientsManager.App.ViewModels;

// TODO: Clean this
public class TableViewModel : Screen
{
    private readonly IOrdersService _ordersService;

    private OrderType _selectedTab;

    public OrderType SelectedTab
    {
        get => _selectedTab;
        set 
        { 
            Set(ref _selectedTab, value);
            LoadTableAsyncCommand.Execute(value);
        }
    }


    #region IsLoading property
    private bool _isLoading = false;
    public bool IsLoading
    {
        get => _isLoading;
        set => Set(ref _isLoading, value);
    }
    #endregion

    #region Orders property 
    private IEnumerable<OrderInfo> _orders;
    public IEnumerable<OrderInfo> Orders
    {
        get => _orders;
        set => Set(ref _orders, value);
    }
    #endregion

    #region Commands
    public ICommand LoadTableAsyncCommand { get; }
    public ICommand AddOrderAsyncCommand { get; } 
    #endregion

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

    public async void EditOrder(OrderInfo orderInfo)
    {
        var command = new UpdateOrderAsyncCommand(this, _ordersService);

        await command.ExecuteAsync(orderInfo);
    }

    public async void DeleteOrder(OrderInfo orderInfo)
    {
        var command = new DeleteOrderAsyncCommand(this, _ordersService);

        await command.ExecuteAsync(orderInfo.Id);
    } 

    public async void ChangeTab(OrderType tabType)
    {
        SelectedTab = tabType;
    }
}
