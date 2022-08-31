using Caliburn.Micro;
using ClientsManager.Infrastructure.Persistence;
using ClientsManager.Infrastructure.Persistence.Builders;
using ClientsManager.Infrastructure.Persistence.Builders.Interfaces;
using ClientsManager.Infrastructure.Persistence.Repositories;
using ClientsManager.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClientsManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        services.AddScoped<IOrdersRepository, OrdersRepository>();
        services.AddScoped<IOrdersTableQueryBuilder, OrdersTableQueryBuilder>();

        return services;
    }
}
