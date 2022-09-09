using ClientsManager.Infrastructure.Persistence.Builders.Interfaces;

namespace ClientsManager.Infrastructure.Persistence.Builders.Extenstions.OrdersTableQueryBuilderExtensions;

public static class GetCountExtension
{
    public static int Count(this IOrdersTableQueryBuilder builder)
    {
        return builder.Query.Count();
    }
}
