using ClientsManager.Domain.Enums;
using ClientsManager.Infrastructure.Persistence.Builders.Interfaces;

namespace ClientsManager.Infrastructure.Persistence.Builders.Extenstions.OrdersTableQueryBuilderExtensions;

public static class WithSearchingExtension
{
    public static IOrdersTableQueryBuilder WithSearching(
        this IOrdersTableQueryBuilder builder,
        SearchOptions option,
        string? parameter)
    {
        return option switch
        {
            SearchOptions.None => builder,
            SearchOptions.ByCustomer => builder
                                .WithCustomer(parameter),
            SearchOptions.ByPhone => builder
                                .WithPhoneNumber(parameter),
            SearchOptions.ByCarModel => builder
                                .WithCarModel(parameter),
            SearchOptions.ByCarNumber => builder
                                .WithCarNumber(parameter),
            SearchOptions.ByVIN => builder
                                .WithVIN(parameter),
            _ => throw new InvalidOperationException(),
        };
    }
}
