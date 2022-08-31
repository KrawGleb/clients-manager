using ClientsManager.Domain.Models.Interfaces;
using ClientsManager.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClientsManager.Infrastructure.Persistence.Repositories;

public class EFRepository<T> : IEFRepository<T> where T : class, IEntity, new()
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _table;

    public EFRepository(ApplicationDbContext context)
    {
        _context = context;
        _table = context.Set<T>();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(bool trackEntities = false)
    {
        var query = trackEntities
            ? _table
            : _table.AsNoTracking();

        return await query.ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id, bool trackEntity = false)
    {
        var query = trackEntity
            ? _table
            : _table.AsNoTracking();

        return await query.FirstOrDefaultAsync(t => t.Id == id);
    }

    public virtual async Task AddRangeAsync(IEnumerable<T> entities)
    {
        _table.AddRange(entities);

        _context.SaveChanges();
    }


    public virtual void Create(T entity)
    {
        _table.Add(entity);

        _context.SaveChanges();
    }

    public virtual void Update(T entity) => _table.Update(entity);

    public virtual void Delete(T entity)
    {
        _table.Remove(entity);
        _context.SaveChanges();
    }

    public virtual void Delete(int id)
    {
        var entity = _context
            .ChangeTracker
            .Entries<T>()
            .FirstOrDefault(entry => entry.Entity.Id == id)?
            .Entity;

        entity ??= new T { Id = id };

        _table.Remove(entity);

        _context.SaveChanges();
    }
}
