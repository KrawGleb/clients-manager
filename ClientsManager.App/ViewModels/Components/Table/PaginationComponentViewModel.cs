using ClientsManager.App.Commands.TableCommands;
using ClientsManager.App.ViewModels.Base;
using System.Windows.Input;

namespace ClientsManager.App.ViewModels.Components.Table;

public class PaginationComponentViewModel : ViewModelBase
{
    #region Properties

    #region ParentRef
    private TableViewModel? _parentRef;

    public TableViewModel? ParentRef
    {
        get => _parentRef;
        set
        {
            _parentRef = value;
            InitCommands();
        }
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

            IsFirstPage = CurrentPageNumber == 1 || CurrentPageNumber == 0;

            ParentRef!.LoadPageAsyncCommand.Execute(CurrentPageNumber);
        }
    }
    #endregion

    #region TotalPagesCount
    private int _totalPagesCount;

    public int TotalPagesCount
    {
        get => _totalPagesCount;
        set
        {
            Set(ref _totalPagesCount, value);

            IsLastPage = CurrentPageNumber == TotalPagesCount;
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

    #endregion

    #region Commands
    public ICommand? NextPageAsyncCommand { get; set; }
	public ICommand? PrevPageAsyncCommand { get; set; } 
	#endregion

	private void InitCommands()
	{
		NextPageAsyncCommand = new NextPageAsyncCommand(ParentRef!);
		PrevPageAsyncCommand = new PrevPageAsyncCommand(ParentRef!);
	}
}
