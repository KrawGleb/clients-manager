using Caliburn.Micro;
using ClientsManager.App.Commands.DataAccessCommands;
using ClientsManager.App.Commands.TableCommands;
using ClientsManager.App.Helpers.Collections;
using ClientsManager.App.Helpers.Models;
using ClientsManager.App.ViewModels.Components;
using ClientsManager.App.ViewModels.Components.Table;
using ClientsManager.Application.Services.Interfaces;
using ClientsManager.Domain.Enums;
using ClientsManager.Domain.Models;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace ClientsManager.App.ViewModels;

public class TableViewModel : Screen
{
    private readonly IOrdersService _ordersService;

    public PaginationComponentViewModel PaginationComponent { get; set; }
    public SearchComponentViewModel SearchComponent { get; set; }

    #region Properties

    #region SelectedTab
    private OrderType _selectedTab;

    public OrderType SelectedTab
    {
        get => _selectedTab;
        set => Set(ref _selectedTab, value);
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
    private WpfObservableRangeCollection<OrderInfo> _orders = new();

    public WpfObservableRangeCollection<OrderInfo> Orders
    {
        get => _orders;
    }
    #endregion

    #region SortBy
    private string _sortBy;

    public string SortBy
    {
        get { return _sortBy; }
        set { _sortBy = value; }
    }
    #endregion

    #region SortOrder
    private string _sortOrder;

    public string SortOrder
    {
        get { return _sortOrder; }
        set { _sortOrder = value; }
    }
    #endregion


    public IEnumerable SelectedItems { get; set; }

    #endregion

    #region Commands
    public ICommand AddOrderAsyncCommand { get; }
    public ICommand InitTableAsyncCommand { get; }
    public ICommand LoadPageAsyncCommand { get; }
    #endregion

    public TableViewModel(
        IOrdersService ordersService,
        PaginationComponentViewModel paginationComponentVM,
        SearchComponentViewModel searchComponent)
    {
        _ordersService = ordersService;

        PaginationComponent = paginationComponentVM;
        SearchComponent = searchComponent;

        PaginationComponent.ParentRef = this;
        SearchComponent.ParentRef = this;

        AddOrderAsyncCommand = new AddOrderAsyncCommand(this, ordersService);
        InitTableAsyncCommand = new InitTableAsyncCommand(this, ordersService);
        LoadPageAsyncCommand = new LoadPageAsyncCommand(this, ordersService);
    }

    public void EditOrder(OrderInfo orderInfo)
    {
        var command = new UpdateOrderAsyncCommand(this, _ordersService);

        command.Execute(orderInfo);
    }

    public void DeleteOrder(OrderInfo orderInfo)
    {
        var command = new DeleteOrderAsyncCommand(this, _ordersService);

        command.Execute(orderInfo.Id);
    }

    public void ChangeTab(OrderType tabType)
    {
        if (tabType == SelectedTab)
        {
            return;
        }

        SelectedTab = tabType;

        InitTableAsyncCommand.Execute(null);
    }

    public void SortTable(object sender, DataGridSortingEventArgs e)
    {
        if (SortBy is not null && SortBy == e.Column.SortMemberPath)
        {
            SortOrder = SortOrder == ListSortDirection.Ascending.ToString()
                ? ListSortDirection.Descending.ToString()
                : ListSortDirection.Ascending.ToString();
        }
        else
        {
            SortBy = e.Column.SortMemberPath;

            SortOrder = (e.Column.SortDirection ?? ListSortDirection.Ascending) == ListSortDirection.Ascending
                ? ListSortDirection.Descending.ToString()
                : ListSortDirection.Ascending.ToString();
        }

        InitTableAsyncCommand.Execute(null);
    }

    public TableItemsParameters GetItemsParameters()
    {
        return new()
        {
            PageSize = PaginationComponent.PageSize,
            PageNumber = PaginationComponent.CurrentPageNumber,
            SearchOption = SearchComponent.SearchOption,
            SearchValue = SearchComponent.SearchValue,
            Tab = SelectedTab,
            SortBy = SortBy,
            SortOrder = SortOrder
        };
    }

    public void SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
    {
        SelectedItems = (sender as DataGrid)?.SelectedItems;
    }
}
