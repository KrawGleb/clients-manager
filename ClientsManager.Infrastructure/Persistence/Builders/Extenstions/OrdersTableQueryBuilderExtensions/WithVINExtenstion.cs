using ClientsManager.Infrastructure.Persistence.Builders.Interfaces;

namespace ClientsManager.Infrastructure.Persistence.Builders.Extenstions.OrdersTableQueryBuilderExtensions;

public static class WithVINExtenstion
{
    public static IOrdersTableQueryBuilder WithVIN(
        this IOrdersTableQueryBuilder builder,
        string? vin)
    {
        if (!string.IsNullOrEmpty(vin))
        {
            builder.AddFilter(e => (e.VIN != null) && e.VIN.StartsWith(vin));
        }

        return builder;
    }
}
