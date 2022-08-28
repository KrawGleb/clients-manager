using Caliburn.Micro;
using ClientsManager.Application.Services;
using ClientsManager.Application.Services.Interfaces;

namespace ClientsManager.Application;

public static class DependencyInjection
{
    public static SimpleContainer AddApplication(this SimpleContainer container)
    {
        container
            .PerRequest<IOrdersService, OrdersService>();

        return container;
    }
}
