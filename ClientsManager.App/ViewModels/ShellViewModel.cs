using Caliburn.Micro;
using ClientsManager.App.Commands.DataAccessCommands;
using ClientsManager.App.Commands.FileCommands;
using ClientsManager.App.Helpers.Models;
using ClientsManager.App.ViewModels.Components.Table;
using ClientsManager.Application.Services.Interfaces;
using System.Threading;
using System.Windows.Input;

namespace ClientsManager.App.ViewModels;

public class ShellViewModel : Conductor<object>
{
    public ShellViewModel(
        IOrdersService orderService,
        ISerializationService serializationService,
        IPrintToPdfService printToPdfService,
        PaginationComponentViewModel paginationComponent,
        SearchComponentViewModel searchComponentVM)
    {
        var tableVM = TableViewModel.LoadTableViewModel(
            orderService,
            paginationComponent,
            searchComponentVM);

        ExportToFileCommand = new ExportToFileAsyncCommand(this, orderService, serializationService);
        ImportFromFileCommand = new ImportFromFileAsyncCommand(this, tableVM, orderService, serializationService);
        ClearOrdersTableCommand = new ClearOrdersTableAsyncCommand(tableVM, orderService);
        PrintAsyncCommand = new PrintAsyncCommand(tableVM, printToPdfService);

        var _ = new NotifyTaskCompletion<object>(ActivateItemAsync(tableVM, CancellationToken.None));
    }

    #region IsLoading 
    private bool _isLoading = false;
    public bool IsLoading
    {
        get => _isLoading;
        set => Set(ref _isLoading, value);
    }
    #endregion

    #region Commands
    public ICommand ExportToFileCommand { get; }
    public ICommand ImportFromFileCommand { get; }
    public ICommand ClearOrdersTableCommand { get; set; }
    public ICommand PrintAsyncCommand { get; set; } 
    #endregion
}
