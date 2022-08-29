using Caliburn.Micro;
using ClientsManager.App.Helpers.Models;
using ClientsManager.Application.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ClientsManager.App.ViewModels;

public class ShellViewModel : Conductor<object>
{
    public ShellViewModel(IOrdersService orderService)
    {

        var tableVM = TableViewModel.LoadTableViewModel(orderService);
        var _ = new NotifyTaskCompletion<object>(ActivateItemAsync(tableVM, CancellationToken.None));
    }
}
