using ClientsManager.Domain.Enums;
using ClientsManager.Domain.Models;

namespace ClientsManager.Application.Services.Interfaces;

public interface IOrdersService
{
    Task<IEnumerable<OrderInfo>> GetAllAsync();
    Task<IEnumerable<OrderInfo>> GetByTypeAsync(OrderType type);
    Task<IEnumerable<OrderInfo>> GetPageAsync(int pageNumber, int pageSize = 25, OrderType type = OrderType.CarWash);
    int GetTotalCountAsync(OrderType type);
    Task<OrderInfo?> GetByIdAsync(int id);
    Task AddAsync(OrderInfo order);
    Task UpdateAsync(OrderInfo order);
    Task DeleteAsync(int id);
}
