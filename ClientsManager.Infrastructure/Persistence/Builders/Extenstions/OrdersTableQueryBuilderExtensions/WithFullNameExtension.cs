using ClientsManager.Infrastructure.Persistence.Builders.Interfaces;

namespace ClientsManager.Infrastructure.Persistence.Builders.Extenstions.OrdersTableQueryBuilderExtensions;

public static class WithFullNameExtension
{
    public static IOrdersTableQueryBuilder WithCustomer(
        this IOrdersTableQueryBuilder builder,
        string? customer)
    {
        if (!string.IsNullOrEmpty(customer))
        {
            builder.AddFilter((e) => e.Customer.Contains(customer));
        }

        return builder;
    }
}
