using ClientsManager.App.Commands.DataAccessCommands;
using ClientsManager.App.Commands.TableCommands;
using ClientsManager.App.Helpers.Collections;
using ClientsManager.App.Helpers.Models;
using ClientsManager.App.ViewModels.Base;
using ClientsManager.App.ViewModels.Components.Table;
using ClientsManager.Application.Services.Interfaces;
using ClientsManager.Domain.Enums;
using ClientsManager.Domain.Models;
using System.Collections;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClientsManager.App.ViewModels;

public class TableViewModel : ViewModelBase
{
    public TableViewModel(
        IOrdersService ordersService,
        PaginationComponentViewModel paginationComponentVM,
        SearchComponentViewModel searchComponent)
    {
        PaginationComponent = paginationComponentVM;
        SearchComponent = searchComponent;

        PaginationComponent.ParentRef = this;
        SearchComponent.ParentRef = this;

        AddOrderAsyncCommand = new AddOrderAsyncCommand(this, ordersService);
        InitTableAsyncCommand = new InitTableCommand(this, ordersService);
        LoadPageAsyncCommand = new LoadPageAsyncCommand(this, ordersService);
        UpdateOrderAsyncCommand = new UpdateOrderAsyncCommand(this, ordersService);
        DeleteOrderAsyncCommand = new DeleteOrderAsyncCommand(this, ordersService);
        ChangeTabCommand = new ChangeTabCommand(this);
    }

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
    private readonly WpfObservableRangeCollection<OrderInfo> _orders = new();

    public WpfObservableRangeCollection<OrderInfo> Orders
    {
        get => _orders;
    }
    #endregion

    #region SortBy
    public string? SortBy { get; set; }
    #endregion

    #region SortOrder
    public string? SortOrder { get; set; }
    #endregion

    #region SelectedItems
    public IEnumerable? SelectedItems { get; set; }
    #endregion

    #endregion

    #region Commands
    public ICommand AddOrderAsyncCommand { get; }
    public ICommand InitTableAsyncCommand { get; }
    public ICommand LoadPageAsyncCommand { get; }
    public ICommand UpdateOrderAsyncCommand { get; }
    public ICommand DeleteOrderAsyncCommand { get; }
    public ICommand ChangeTabCommand { get; }
    #endregion

    public void SortTable(object sender, DataGridSortingEventArgs e)
    {
        if (SortBy is not null && SortBy == e.Column.SortMemberPath)
        {
            SortOrder = SortOrder == ListSortDirection.Ascending.ToString()
                ? ListSortDirection.Ascending.ToString()
                : ListSortDirection.Descending.ToString();
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
