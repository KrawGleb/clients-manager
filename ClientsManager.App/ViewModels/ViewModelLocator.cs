using Microsoft.Extensions.DependencyInjection;

namespace ClientsManager.App.ViewModels;

public class ViewModelLocator
{
    public ShellViewModel ShellViewModel
        => App.Host!.Services.GetRequiredService<ShellViewModel>();
}
