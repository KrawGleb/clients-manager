using Caliburn.Micro;
using ClientsManager.App.Commands.DataAccessCommands;
using ClientsManager.App.Commands.TableCommands;
using ClientsManager.App.Helpers.Models;
using ClientsManager.Application.Services.Interfaces;
using ClientsManager.Domain.Enums;
using ClientsManager.Domain.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace ClientsManager.App.ViewModels;

public class TableViewModel : Screen
{
    private readonly IOrdersService _ordersService;

    #region Properties

    #region SelectedTab
    private OrderType _selectedTab;

    public OrderType SelectedTab
    {
        get => _selectedTab;
        set
        {
            Set(ref _selectedTab, value);
        }
    }
    #endregion

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
        set
        {
            value = value <= TotalPagesCount 
                ? value 
                : TotalPagesCount;

            Set(ref _currentPageNumber, value);

            IsFirstPage = CurrentPageNumber == 1;
            IsLastPage = CurrentPageNumber == TotalPagesCount;

            LoadPageAsyncCommand.Execute(CurrentPageNumber);
        }
    }
    #endregion

    #region PageSize
    private int _pageSize = 10;

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
        get => _totalPagesCount;
        set => Set(ref _totalPagesCount, value);
    }
    #endregion

    #region IsFirstPage
    private bool _isFirstPage;

    public bool IsFirstPage
    {
        get => _isFirstPage;
        set => Set(ref _isFirstPage, value);
    }
    #endregion

    #region IsLastPage
    private bool _isLastPage;

    public bool IsLastPage
    {
        get => _isLastPage;
        set => Set(ref _isLastPage, value);
    }
    #endregion

    #region SearchValue
    private string _searchValue;

    public string SearchValue
    {
        get { return _searchValue; }
        set { _searchValue = value; }
    }
    #endregion

    #region SearchOption
    public SearchOptions SearchOption { get; set; }
    # endregion

    #endregion

    #region Commands
    public ICommand AddOrderAsyncCommand { get; }
    public ICommand NextPageAsyncCommand { get; }
    public ICommand PrevPageAsyncCommand { get; }
    public ICommand InitTableAsyncCommand { get; }
    public ICommand LoadPageAsyncCommand { get; }
    #endregion

    public TableViewModel(IOrdersService ordersService)
    {
        _ordersService = ordersService;

        AddOrderAsyncCommand = new AddOrderAsyncCommand(this, ordersService);
        NextPageAsyncCommand = new NextPageAsyncCommand(this, ordersService);
        PrevPageAsyncCommand = new PrevPageAsyncCommand(this, ordersService);
        InitTableAsyncCommand = new InitTableAsyncCommand(this, ordersService);
        LoadPageAsyncCommand = new LoadPageAsyncCommand(this, ordersService);
    }

    public static TableViewModel LoadTableViewModel(IOrdersService ordersService)
    {
        var tableViewModel = new TableViewModel(ordersService);

        tableViewModel.InitTableAsyncCommand.Execute(ordersService);

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

        InitTableAsyncCommand.Execute(null);
    }

    public TableItemsParameters GetItemsParameters()
    {
        return new()
        {
            PageSize = PageSize,
            PageNumber = CurrentPageNumber,
            SearchOption = SearchOption,
            SearchValue = SearchValue,
            Tab = SelectedTab,
        };
    }
}
