using ClientsManager.Domain.Enums;
using ClientsManager.Domain.Models;

namespace ClientsManager.Application.Services.Interfaces;

public interface IOrdersService
{
    Task<IEnumerable<OrderInfo>> GetAllAsync();
    Task<IEnumerable<OrderInfo>> GetByTypeAsync(OrderType type);
    Task<OrderInfo?> GetByIdAsync(int id);
    Task AddAsync(OrderInfo order);
    Task AddRangeAsync(IEnumerable<OrderInfo> orders);
    Task UpdateAsync(OrderInfo order);
    void Delete(int id);
    Task ClearAsync();
    Task<IEnumerable<OrderInfo>> GetPageAsync(
        int pageNumber,
        int pageSize = 25,
        OrderType type = OrderType.CarWash,
        SearchOptions searchOption = SearchOptions.None,
        string searchParameter = "",
        string sortBy = "",
        string sortOrder = "");
    int GetTotalCountAsync(
        OrderType type,
        SearchOptions searchOption,
        string? searchParameter);
}
