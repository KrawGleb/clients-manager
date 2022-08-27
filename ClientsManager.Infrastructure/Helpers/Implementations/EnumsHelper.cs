using ClientsManager.Domain.Enums;
using ClientsManager.Infrastructure.Helpers.Interfaces;

namespace ClientsManager.Infrastructure.Helpers.Implementations;

public class EnumsHelper : IEnumsHelper
{
    public OrderType ConvertStringToOrderType(string value)
    {
        return value switch
        {
            "Автомойка" => OrderType.CarWash,
            "Автосервис" => OrderType.CarService,
            _ => throw new InvalidOperationException(),
        };
    }
}
