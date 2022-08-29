using Caliburn.Micro;
using ClientsManager.App.Commands.DataAccessCommands;
using ClientsManager.App.Commands.TableCommands;
using ClientsManager.App.ViewModels.Dialogs;
using ClientsManager.Application.Services.Interfaces;
using ClientsManager.Domain.Enums;
using ClientsManager.Domain.Models;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Windows.Input;

namespace ClientsManager.App.ViewModels;

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

    #region Properties

    #region IsLoading 
    private bool _isLoading = false;
    public bool IsLoading
    {
        get => _isLoading;
        set => Set(ref _isLoading, value);
    }
    #endregion

    #region Orders 
    private IEnumerable<OrderInfo> _orders;
    public IEnumerable<OrderInfo> Orders
    {
        get => _orders;
        set => Set(ref _orders, value);
    }
    #endregion

    #region CurrentPageNumber
    private int _currentPageNumber = 0;

    public int CurrentPageNumber
    {
        get => _currentPageNumber;
        set => Set(ref _currentPageNumber, value);
    }
    #endregion

    #region PageSize
    private int _pageSize = 2;

    public int PageSize
    {
        get => _pageSize;
        set => Set(ref _pageSize, value);
    }
    #endregion

    #region TotalPagesCount
    private int _totalPagesCount = 10;

    public int TotalPagesCount
    {
        get { return _totalPagesCount; }
        set { _totalPagesCount = value; }
    }
    #endregion

    #region IsFirstPage
    public bool IsFirstPage
    {
        get => CurrentPageNumber == 1;
    }
    #endregion

    #region IsLastPage
    public bool IsLastPage
    {
        get => CurrentPageNumber == TotalPagesCount;
    } 
    #endregion

    #endregion

    #region Commands
    public ICommand LoadTableAsyncCommand { get; }
    public ICommand AddOrderAsyncCommand { get; } 
    public ICommand NextPageAsyncCommand { get; }
    public ICommand PrevPageAsyncCommand { get; }
    public ICommand GetTotalPagesCountAsyncCommand { get; }
    #endregion

    public TableViewModel(IOrdersService ordersService)
    {
        _ordersService = ordersService;

        LoadTableAsyncCommand = new LoadTableAsyncCommand(this, ordersService);
        AddOrderAsyncCommand = new AddOrderAsyncCommand(this, ordersService);
        NextPageAsyncCommand = new NextPageAsyncCommand(this, ordersService);
        PrevPageAsyncCommand = new PrevPageAsyncCommand(this, ordersService);
        //GetTotalPagesCountAsyncCommand = new GetTotalPagesCountAsyncCommand(this, ordersService);
    }

    public static TableViewModel LoadTableViewModel(IOrdersService ordersService)
    {
        var tableViewModel = new TableViewModel(ordersService);

        //tableViewModel.GetTotalPagesCountAsyncCommand.Execute(null);
        tableViewModel.NextPageAsyncCommand.Execute(null);

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
        if (tabType == SelectedTab)
        {
            return;
        }

        SelectedTab = tabType;
    }
}
