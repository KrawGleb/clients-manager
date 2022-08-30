using ClientsManager.Infrastructure.Persistence.Builders.Interfaces;

namespace ClientsManager.Infrastructure.Persistence.Builders.Extenstions.OrdersTableQueryBuilderExtensions;

public static class WithPhoneNumberExtension
{
    public static IOrdersTableQueryBuilder WithPhoneNumber(this IOrdersTableQueryBuilder builder, string phoneNumber)
    {
        if (!string.IsNullOrEmpty(phoneNumber))
        {
            builder.AddFilter(e => e.PhoneNumber.StartsWith(phoneNumber));
        }

        return builder;
    }

    // ??
    private static string CutPhoneNumber(string phoneNumber)
    {
        return "";
    }
}
