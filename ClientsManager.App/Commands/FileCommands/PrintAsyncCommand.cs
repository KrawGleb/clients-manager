using ClientsManager.App.ViewModels;
using ClientsManager.Application.Services.Interfaces;
using Ookii.Dialogs.Wpf;
using System.Threading.Tasks;

namespace ClientsManager.App.Commands.FileCommands;

public class PrintAsyncCommand : AsyncCommandBase
{
    private readonly TableViewModel _tableViewModel;
    private readonly IPrintToPdfService _printService;

    public PrintAsyncCommand(
        TableViewModel tableViewModel,
        IPrintToPdfService printService)
    {
        _tableViewModel = tableViewModel;
        _printService = printService;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        var dialog = new VistaSaveFileDialog();
        dialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
        dialog.DefaultExt = ".pdf";
        var result = dialog.ShowDialog() ?? false;

        if (result)
        {
            _tableViewModel.IsLoading = true;

            var selectedItems = _tableViewModel.SelectedItems;
            if (selectedItems is not null)
            {
                var document = _printService.CreatePdfDocument(selectedItems);
                document.Save(dialog.FileName);
            }
            _tableViewModel.IsLoading = false;
        }

    }
}
