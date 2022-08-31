using ClientsManager.App.ViewModels;
using ClientsManager.App.ViewModels.Components.Table;
using ClientsManager.App.ViewModels.Dialogs;
using ClientsManager.App.Views;
using ClientsManager.Application;
using ClientsManager.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Linq;
using System.Windows;

namespace ClientsManager.App
{
    public partial class App : System.Windows.Application
    {
        private static IHost _host;

        public static IHost? Host => _host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();


        protected override async void OnStartup(StartupEventArgs e)
        {
            var host = Host;
            base.OnStartup(e);

            await host.StartAsync().ConfigureAwait(false);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            var host = Host;
            await host.StopAsync().ConfigureAwait(false);

            host.Dispose();
            _host = null;
        }
        
        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            var configuration = host.Configuration;

            services.AddInfrastructure(configuration);
            services.AddApplication();

            // View models
            services.AddSingleton<ShellViewModel>();
            services.AddSingleton<TableViewModel>();

            // Dialogs
            services.AddSingleton<AddOrderDialogViewModel>();
            services.AddSingleton<EditOrderDialogViewModel>();
            services.AddSingleton<WarningMessageDialogViewModel>();

            // Components
            services.AddSingleton<PaginationComponentViewModel>();
            services.AddSingleton<SearchComponentViewModel>();
        }

        public static string CurrentDirectory => Environment.CurrentDirectory;
    }
}
