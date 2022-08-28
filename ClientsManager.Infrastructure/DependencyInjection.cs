using Caliburn.Micro;
using ClientsManager.Infrastructure.Persistence;
using ClientsManager.Infrastructure.Persistence.Repositories;
using ClientsManager.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ClientsManager.Infrastructure;

public static class DependencyInjection
{
    public static SimpleContainer AddInfrastructure(this SimpleContainer container, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .Options;
        var dbContext = new ApplicationDbContext(options);

        container.Instance(dbContext);

        container
            .PerRequest<IOrdersRepository, OrdersRepository>();

        return container;
    }
}
