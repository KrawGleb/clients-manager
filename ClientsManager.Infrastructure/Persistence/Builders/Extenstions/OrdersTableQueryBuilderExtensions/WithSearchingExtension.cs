using ClientsManager.Domain.Enums;
using ClientsManager.Infrastructure.Persistence.Builders.Interfaces;

namespace ClientsManager.Infrastructure.Persistence.Builders.Extenstions.OrdersTableQueryBuilderExtensions;

public static class WithSearchingExtension
{
    public static IOrdersTableQueryBuilder WithSearching(this IOrdersTableQueryBuilder builder, SearchOptions option, string? parameter)
    {
        return option switch
        {
            SearchOptions.None => builder,
            SearchOptions.ByFullName => builder
                                .WithFullName(parameter),
            SearchOptions.ByPhone => builder
                                .WithPhoneNumber(parameter),
            SearchOptions.ByCarModel => builder
                                .WithCarModel(parameter),
            SearchOptions.ByCarNumber => builder
                                .WithCarNumber(parameter),
            _ => throw new InvalidOperationException(),
        };
    }
}
