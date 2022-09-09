using ClientsManager.Infrastructure.Persistence.Builders.Helpers;
using ClientsManager.Infrastructure.Persistence.Builders.Interfaces;

namespace ClientsManager.Infrastructure.Persistence.Builders.Extenstions.OrdersTableQueryBuilderExtensions;

public static class OrderByExtension
{
    public static IOrdersTableQueryBuilder OrderBy(
        this IOrdersTableQueryBuilder builder,
        string sortBy,
        string sortOrder)
    {
        if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(sortOrder))
        {
            builder.Query = builder.Query.OrderByCustom(sortBy, sortOrder);
        }

        return builder;
    }
}
