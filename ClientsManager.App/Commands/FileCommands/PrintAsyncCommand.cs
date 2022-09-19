using ClientsManager.App.ViewModels;
using ClientsManager.Application.Services.Interfaces;
using Ookii.Dialogs.Wpf;
using System;
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

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
        => true;

    public override async Task ExecuteAsync(object? parameter)
    {
        var selectedItem = _tableViewModel.SelectedItem;
        if (selectedItem is null)
        {
            return;
        }

        var dialog = new VistaSaveFileDialog
        {
            Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*",
            DefaultExt = ".pdf",
            FileName = $"Акт №{selectedItem.Id}"
        };

        var result = dialog.ShowDialog() ?? false;

        if (result)
        {
            _tableViewModel.IsLoading = true;

            await Task.Run(() => _printService.CreatePdfDocument(selectedItem, dialog.FileName));

            _tableViewModel.IsLoading = false;
        }
    }
}
