using ClientsManager.Domain.Enums;
using ClientsManager.Domain.Models;

namespace ClientsManager.Infrastructure.Persistence.Repositories.Interfaces;

public interface IOrdersRepository : IEFRepository<OrderInfo>
{
    Task<IEnumerable<OrderInfo>> GetByTypeAsync(OrderType type, bool trackEntities = false);
    Task<IEnumerable<OrderInfo>> GetSliceAsync(int sliceNumber, int sliceSize, OrderType type);
    Task UpdateAsync(OrderInfo entity);
    Task<int> GetTotalCount(OrderType type);
}
