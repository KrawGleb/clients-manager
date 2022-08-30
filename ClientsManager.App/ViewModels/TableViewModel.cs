﻿using Caliburn.Micro;
using ClientsManager.App.Commands.DataAccessCommands;
using ClientsManager.App.Commands.TableCommands;
using ClientsManager.App.Helpers.Models;
using ClientsManager.App.ViewModels.Components;
using ClientsManager.App.ViewModels.Components.Table;
using ClientsManager.Application.Services.Interfaces;
using ClientsManager.Domain.Enums;
using ClientsManager.Domain.Models;
using System.Collections.Generic;
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

    public static TableViewModel LoadTableViewModel(
        IOrdersService ordersService, 
        PaginationComponentViewModel paginationComponentVM,
        SearchComponentViewModel searchComponentVM)
    {
        var tableViewModel = new TableViewModel(
            ordersService,
            paginationComponentVM,
            searchComponentVM);

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
            PageSize = PaginationComponent.PageSize,
            PageNumber = PaginationComponent.CurrentPageNumber,
            SearchOption = SearchComponent.SearchOption,
            SearchValue = SearchComponent.SearchValue,
            Tab = SelectedTab,
        };
    }
}
