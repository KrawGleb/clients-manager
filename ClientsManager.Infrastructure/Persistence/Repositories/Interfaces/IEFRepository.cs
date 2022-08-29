using ClientsManager.Domain.Models.Interfaces;

namespace ClientsManager.Infrastructure.Persistence.Repositories.Interfaces;
public interface IEFRepository<T> where T : class, IEntity, new()
{
    void Create(T entity);
    void Delete(int id);
    void Delete(T entity);
    Task<IEnumerable<T>> GetAllAsync(bool trackEntities = false);
    Task<T?> GetByIdAsync(int id, bool trackEntity = false);
    void Update(T entity);
}