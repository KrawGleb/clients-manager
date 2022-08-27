using Caliburn.Micro;
using ClientsManager.App.ViewModels;
using System.Threading.Tasks;
using System.Windows;

namespace ClientsManager.App;

public class Bootstrapper : BootstrapperBase
{
	private readonly SimpleContainer _container = new();

	public Bootstrapper()
	{
		Initialize();
	}

	protected override void Configure()
	{

	}

	protected override void OnStartup(object sender, StartupEventArgs e)
	{
		DisplayRootViewForAsync<ShellViewModel>();
	}
}
