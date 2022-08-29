using ClientsManager.Domain.Models;

namespace ClientsManager.Application.Services.Interfaces;

public interface IOrdersService
{
    Task<IEnumerable<OrderInfo>> GetAllAsync();
    Task<OrderInfo?> GetByIdAsync(int id);
    Task AddAsync(OrderInfo order);
    Task UpdateAsync(OrderInfo order);
    Task DeleteAsync(int id);
}
