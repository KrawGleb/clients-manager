using Caliburn.Micro;
using System.Threading;
using System.Threading.Tasks;

namespace ClientsManager.App.ViewModels;

public class ShellViewModel : Conductor<object>
{
    private readonly TableViewModel _tableVM;

    public ShellViewModel(TableViewModel tableVM, AddOrderDialogViewModel addOrderDialogVM)
    {
        _tableVM = tableVM;

        Task.WaitAll(ActivateItemAsync(_tableVM, CancellationToken.None));
    }
}
