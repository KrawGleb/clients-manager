using ClientsManager.Domain.Enums;
using ClientsManager.Domain.Models;

namespace ClientsManager.Infrastructure.Persistence.Repositories.Interfaces;

public interface IOrdersRepository : IEFRepository<OrderInfo>
{
    Task<IEnumerable<OrderInfo>> GetByTypeAsync(OrderType type, bool trackEntities = false);
    Task UpdateAsync(OrderInfo entity);
    int GetTotalCount(OrderType type);
    Task ClearAsync();
}
