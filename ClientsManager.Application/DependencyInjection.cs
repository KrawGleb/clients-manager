using ClientsManager.Application.Services;
using ClientsManager.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ClientsManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ISearchingService, SearchingService>();
        services.AddScoped<IOrdersService, OrdersService>();
        services.AddScoped<ISerializationService, SerializationService>();
        services.AddScoped<IPrintToPdfService, PrintToPdfService>();

        return services;
    }
}
