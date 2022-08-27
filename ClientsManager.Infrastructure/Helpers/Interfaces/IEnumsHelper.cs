using ClientsManager.Domain.Enums;

namespace ClientsManager.Infrastructure.Helpers.Interfaces;
public interface IEnumsHelper
{
    OrderType ConvertStringToOrderType(string value);
}