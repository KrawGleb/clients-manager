using Caliburn.Micro;
using ClientsManager.Domain.Enums;
using System.Windows.Input;

namespace ClientsManager.App.ViewModels.Components.Table;

public class SearchComponentViewModel : Screen
{
    #region Properties

    #region ParentRef
    private TableViewModel _parentRef;

    public TableViewModel ParentRef
    {
        get => _parentRef;
        set => _parentRef = value;
        
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
    #endregion

    #endregion

    #region Commands
    public ICommand LoadPageAsyncCommand
    {
        get => ParentRef.LoadPageAsyncCommand;
    }
    #endregion
}
