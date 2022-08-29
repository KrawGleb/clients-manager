using ClientsManager.Domain.Models;

namespace ClientsManager.Application.Services.Interfaces;

public interface IOrdersService
{
    Task<IEnumerable<OrderInfo>> GetAllAsync();
    Task AddAsync(OrderInfo order);
}
