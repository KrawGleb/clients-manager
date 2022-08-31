using ClientsManager.App.ViewModels.Components.Table;
using ClientsManager.App.ViewModels.Dialogs;
using Microsoft.Extensions.DependencyInjection;

namespace ClientsManager.App.ViewModels;

public class ViewModelLocator
{
    public ShellViewModel ShellViewModel
        => App.Host!.Services.GetRequiredService<ShellViewModel>();

    public TableViewModel TableViewModel
        => App.Host!.Services.GetRequiredService<TableViewModel>();

    public PaginationComponentViewModel PaginationComponentViewModel
        => App.Host!.Services.GetRequiredService<PaginationComponentViewModel>();

    public SearchComponentViewModel SearchComponentViewModel
        => App.Host!.Services.GetRequiredService<SearchComponentViewModel>();

    public AddOrderDialogViewModel AddOrderDialogViewModel
        => App.Host!.Services.GetRequiredService<AddOrderDialogViewModel>();

    public EditOrderDialogViewModel EditOrderDialogViewModel
        => App.Host!.Services.GetRequiredService<EditOrderDialogViewModel>();

    public WarningMessageDialogViewModel WarningMessageDialogViewModel
        => App.Host!.Services.GetRequiredService<WarningMessageDialogViewModel>();
}
