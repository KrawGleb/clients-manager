using ClientsManager.Domain.Models.Interfaces;

namespace ClientsManager.Infrastructure.Persistence.Repositories.Interfaces;
public interface IEFRepository<T> where T : class, IEntity, new()
{
    Task CreateAsync(T entity);
    Task DeleteAsync(int id);
    Task DeleteAsync(T entity);
    Task<IEnumerable<T>> GetAllAsync(bool trackEntities = false);
    Task<T?> GetByIdAsync(int id, bool trackEntity = false);
    Task AddRangeAsync(IEnumerable<T> entities);
    void Update(T entity);
}