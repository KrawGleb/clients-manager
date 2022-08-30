using ClientsManager.Infrastructure.Persistence.Builders.Interfaces;

namespace ClientsManager.Infrastructure.Persistence.Builders.Extenstions.OrdersTableQueryBuilderExtensions;

public static class WithPaginationExtension
{
    public static IOrdersTableQueryBuilder WithPagination(this IOrdersTableQueryBuilder builder, int pageNumber, int pageSize)
    {
        if (pageNumber >= 0 && pageSize >= 0)
        {
            builder.Query = builder.Query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }

        return builder;
    }
}
