using ClientsManager.App.ViewModels;
using ClientsManager.Application.Services.Interfaces;
using ClientsManager.Domain.Models;
using Ookii.Dialogs.Wpf;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientsManager.App.Commands.FileCommands;

public class ExportToFileAsyncCommand : AsyncCommandBase
{
    private readonly ShellViewModel _shellViewModel;
    private readonly IOrdersService _ordersService;
    private readonly ISerializationService _serializationService;

    public ExportToFileAsyncCommand(
        ShellViewModel shellViewModel,
        IOrdersService ordersService,
        ISerializationService serializationService)
    {
        _shellViewModel = shellViewModel;
        _ordersService = ordersService;
        _serializationService = serializationService;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        var dialog = new VistaSaveFileDialog();
        dialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
        dialog.DefaultExt = ".json";
        var result = dialog.ShowDialog() ?? false; 

        if (result)
        {
            _shellViewModel.IsLoading = true;

            var orders = await _ordersService.GetAllAsync();
            
            await _serializationService.SerializeToFileAsync(orders, dialog.FileName);
            await Task.Delay(2000);
        
            _shellViewModel.IsLoading = false;
        }

    }
}
