using ClientsManager.Domain.Enums;
using ClientsManager.Domain.Models;

namespace ClientsManager.Application.Services.Interfaces;
public interface ISearchingService
{
    Func<OrderInfo, string, bool> ConvertSearchOptionToPredicate(SearchOptions searchOption);
}