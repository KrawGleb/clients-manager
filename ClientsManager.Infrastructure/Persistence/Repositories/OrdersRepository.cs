using ClientsManager.Domain.Models;
using ClientsManager.Infrastructure.Persistence.Repositories.Interfaces;

namespace ClientsManager.Infrastructure.Persistence.Repositories;

public class OrdersRepository : EFRepository<OrderInfo>, IOrdersRepository
{
    public OrdersRepository(ApplicationDbContext context) : base(context)
    { }
}
