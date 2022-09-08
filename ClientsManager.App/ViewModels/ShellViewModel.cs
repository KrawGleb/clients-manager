using ClientsManager.App.Commands.DataAccessCommands;
using ClientsManager.App.Commands.FileCommands;
using ClientsManager.App.ViewModels.Base;
using ClientsManager.App.ViewModels.Components.Table;
using ClientsManager.Application.Services.Interfaces;
using System.Windows.Input;

namespace ClientsManager.App.ViewModels;

public class ShellViewModel : ViewModelBase
{
    public ShellViewModel(
        IOrdersService orderService,
        ISerializationService serializationService,
        IPrintToPdfService printToPdfService,
        PaginationComponentViewModel paginationComponent,
        SearchComponentViewModel searchComponentVM,
        TableViewModel tableViewModel)
    {
        Table = tableViewModel;

        tableViewModel.InitTableAsyncCommand.Execute(null);

        ExportToFileCommand = new ExportToFileAsyncCommand(this, orderService, serializationService);
        ImportFromFileCommand = new ImportFromFileAsyncCommand(this, Table, orderService, serializationService);
        ClearOrdersTableCommand = new ClearOrdersTableAsyncCommand(Table, orderService);
        PrintAsyncCommand = new PrintAsyncCommand(Table, printToPdfService);
    }

    public TableViewModel Table { get; set; }


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
