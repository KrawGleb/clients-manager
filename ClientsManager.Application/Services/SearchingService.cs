using ClientsManager.Application.Services.Interfaces;
using ClientsManager.Domain.Enums;
using ClientsManager.Domain.Models;

namespace ClientsManager.Application.Services;

public class SearchingService : ISearchingService
{
    public Func<OrderInfo, string, bool> ConvertSearchOptionToPredicate(SearchOptions searchOption)
    {
        return searchOption switch
        {
            SearchOptions.None => (_, _) => true,
            SearchOptions.ByFullName => (order, fullName) => string.Join(" ", order.FirstName, order.LastName, order.LastName).Contains(fullName),
            SearchOptions.ByCarModel => (order, model) => order.CarModel!.StartsWith(model),
            SearchOptions.ByCarNumber => (order, carNumber) => order.CarNumber!.StartsWith(carNumber),
            SearchOptions.ByPhone => (order, phone) =>
                            {
                                var targetPhone = order.PhoneNumber!
                                    .Replace(" ", "")
                                    .Replace("(", "")
                                    .Replace(")", "")
                                    .Replace("+", "")
                                    .Replace("-", "")
                                    .Replace("+375", "");

                                var searchPhone = phone
                                    .Replace(" ", "")
                                    .Replace("(", "")
                                    .Replace(")", "")
                                    .Replace("+", "")
                                    .Replace("-", "")
                                    .Replace("+375", "");

                                return targetPhone.StartsWith(searchPhone);
                            }

            ,
            _ => throw new InvalidOperationException(),
        };
    }
}
