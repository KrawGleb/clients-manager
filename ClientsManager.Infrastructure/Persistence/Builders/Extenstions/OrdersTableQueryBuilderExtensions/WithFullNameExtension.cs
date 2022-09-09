using ClientsManager.Infrastructure.Persistence.Builders.Interfaces;

namespace ClientsManager.Infrastructure.Persistence.Builders.Extenstions.OrdersTableQueryBuilderExtensions;

public static class WithFullNameExtension
{
    public static IOrdersTableQueryBuilder WithFullName(
        this IOrdersTableQueryBuilder builder,
        string? fullName)
    {
        if (!string.IsNullOrEmpty(fullName))
        {
            builder.AddFilter((e) => (e.FirstName + " " + e.LastName + " " + e.AdditionalName).Contains(fullName));
        }

        return builder;
    }
}
