using Caliburn.Micro;
using ClientsManager.Application.Services;
using ClientsManager.Application.Services.Interfaces;

namespace ClientsManager.Application;

public static class DependencyInjection
{
    public static SimpleContainer AddApplication(this SimpleContainer container)
    {
        container
            .PerRequest<ISearchingService, SearchingService>()
            .PerRequest<IOrdersService, OrdersService>()
            .PerRequest<ISerializationService, SerializationService>();

        return container;
    }
}
