using Caliburn.Micro;
using ClientsManager.App.Helpers.Models;
using ClientsManager.App.ViewModels.Components.Table;
using ClientsManager.Application.Services.Interfaces;
using System.Threading;

namespace ClientsManager.App.ViewModels;

public class ShellViewModel : Conductor<object>
{
    public ShellViewModel(
        IOrdersService orderService,
        PaginationComponentViewModel paginationComponent,
        SearchComponentViewModel searchComponentVM)
    {
        var tableVM = TableViewModel.LoadTableViewModel(
            orderService, 
            paginationComponent, 
            searchComponentVM);

        var _ = new NotifyTaskCompletion<object>(ActivateItemAsync(tableVM, CancellationToken.None));
    }
}
