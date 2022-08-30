using ClientsManager.Domain.Enums;
using ClientsManager.Infrastructure.Persistence.Builders.Interfaces;

namespace ClientsManager.Infrastructure.Persistence.Builders.Extenstions.OrdersTableQueryBuilderExtensions;

public static class WithTypeExtension
{
    public static IOrdersTableQueryBuilder WithType(this IOrdersTableQueryBuilder builder, OrderType type)
    {
        builder.AddFilter(e => e.OrderType == type);

        return builder;
    }
}
