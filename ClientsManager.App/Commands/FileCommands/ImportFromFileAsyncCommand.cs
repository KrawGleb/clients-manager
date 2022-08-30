using ClientsManager.App.ViewModels;
using ClientsManager.Application.Services.Interfaces;
using ClientsManager.Domain.Models;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.App.Commands.FileCommands;

public class ImportFromFileAsyncCommand : AsyncCommandBase
{
    private readonly ShellViewModel _shellViewModel;
    private readonly TableViewModel _tableViewModel;
    private readonly IOrdersService _ordersService;
    private readonly ISerializationService _serializationService;

    public ImportFromFileAsyncCommand(
        ShellViewModel shellViewModel,
        TableViewModel tableViewModel,
        IOrdersService ordersService,
        ISerializationService serializationService)
    {
        _shellViewModel = shellViewModel;
        _tableViewModel = tableViewModel;
        _ordersService = ordersService;
        _serializationService = serializationService;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        var dialog = new VistaOpenFileDialog();
        dialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
        dialog.DefaultExt = ".json";
        var result = dialog.ShowDialog() ?? false;

        if (result)
        {
            _shellViewModel.IsLoading = true;

            var objects = await _serializationService.DeserializeFromFileAsync<List<OrderInfo>>(dialog.FileName);
            objects?.ForEach(o => o.Id = default);
            await _ordersService.AddRangeAsync(objects);

            GC.Collect();

            _shellViewModel.IsLoading = false;

            _tableViewModel.InitTableAsyncCommand.Execute(null);
        }


    }
}
