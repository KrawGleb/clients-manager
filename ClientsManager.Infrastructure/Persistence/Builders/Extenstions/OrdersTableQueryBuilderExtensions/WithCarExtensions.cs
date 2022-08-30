using ClientsManager.Infrastructure.Persistence.Builders.Interfaces;

namespace ClientsManager.Infrastructure.Persistence.Builders.Extenstions.OrdersTableQueryBuilderExtensions;

public static class WithCarExtensions
{
    public static IOrdersTableQueryBuilder WithCarModel(this IOrdersTableQueryBuilder builder, string carModel)
    {
        if (!string.IsNullOrEmpty(carModel))
        {
            builder.AddFilter(e => e.CarModel.StartsWith(carModel));
        }

        return builder;
    }

    public static IOrdersTableQueryBuilder WithCarNumber(this IOrdersTableQueryBuilder builder, string carNumber)
    {
        if (!string.IsNullOrEmpty(carNumber))
        {
            builder.AddFilter(e => e.CarNumber.StartsWith(carNumber));
        }

        return builder;
    }
}
