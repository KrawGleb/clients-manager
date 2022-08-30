using ClientsManager.Domain.Enums;
using ClientsManager.Domain.Models;
using ClientsManager.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClientsManager.Infrastructure.Persistence.Repositories;

public class OrdersRepository : EFRepository<OrderInfo>, IOrdersRepository
{
    public OrdersRepository(ApplicationDbContext context) : base(context)
    { }

    public async Task<IEnumerable<OrderInfo>> GetByTypeAsync(OrderType type, bool trackEntities = false)
    {
        var query = trackEntities
            ? _table
            : _table.AsNoTracking();

        return await _table
            .Where(o => o.OrderType == type)
            .ToListAsync();
    }

    public async Task UpdateAsync(OrderInfo entity)
    {
        var oldEntity = await _table.FirstOrDefaultAsync(e => e.Id == entity.Id);

        if (oldEntity is null)
        {
            throw new InvalidOperationException();
        }

        _context.Entry(oldEntity).CurrentValues.SetValues(entity);

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<OrderInfo>> GetSliceAsync(int sliceNumber, int sliceSize, OrderType type)
    {
        return await _table
            .Where(e => e.OrderType == type)
            .Skip((sliceNumber - 1) * sliceSize)
            .Take(sliceSize)
            .ToListAsync();
    }

    public int GetTotalCount(OrderType type)
    {
        return _table.Count(e => e.OrderType == type);
    }

}
