using ClientsManager.Application.Services.Interfaces;
using ClientsManager.Domain.Models;
using ClientsManager.Infrastructure.Persistence;
using ClientsManager.Infrastructure.Persistence.Repositories.Interfaces;

namespace ClientsManager.Application.Services;

public class OrdersService : IOrdersService
{
	private readonly ApplicationDbContext _context;
	private readonly IOrdersRepository _repository;

	public OrdersService(
		ApplicationDbContext context,
		IOrdersRepository repository)
	{
		_context = context;
		_repository = repository;
	}

    public async Task<IEnumerable<OrderInfo>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task AddAsync(OrderInfo order)
	{
		_repository.Create(order);

		await _context.SaveChangesAsync();
	}
}
