using ClientsManager.App.Commands.TableCommands;
using ClientsManager.Application.Services.Interfaces;
using ClientsManager.Domain.Enums;
using System.Windows.Input;

namespace ClientsManager.App.ViewModels.Components.Table;

public class SearchComponentViewModel
{
    #region Properties

    public TableViewModel? ParentRef { get; set; }
    
    public string? SearchValue { get; set; }
    
    public SearchOptions SearchOption { get; set; }
    
    #endregion

    #region Commands
    public ICommand LoadPageAsyncCommand { get => ParentRef!.LoadPageAsyncCommand; }

    public ICommand SearchOrdersAsyncCommand { get => ParentRef!.SearchOrdersAsyncCommand; }

    #endregion
}
