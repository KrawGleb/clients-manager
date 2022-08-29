using ClientsManager.Domain.Enums;
using ClientsManager.Domain.Models;
using ClientsManager.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClientsManager.Infrastructure.Persistence.Repositories;

public class OrdersRepository : EFRepository<OrderInfo>, IOrdersRepository
{
    public OrdersRepository(ApplicationDbContext context) : base(context)
    {
    
    }

    public async Task<IEnumerable<OrderInfo>> GetByTypeAsync(OrderType type, bool trackEntities = false)
    {
        var query = trackEntities
            ? _table
            : _table.AsNoTracking();

        return await _table
            .Where(o => o.OrderType == type)
            .ToListAsync();
    }

}
