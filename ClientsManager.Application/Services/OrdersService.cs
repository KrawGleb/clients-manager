using ClientsManager.Application.Services.Interfaces;
using ClientsManager.Domain.Enums;
using ClientsManager.Domain.Models;
using ClientsManager.Infrastructure.Persistence;
using ClientsManager.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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

	public async Task<IEnumerable<OrderInfo>> GetByTypeAsync(OrderType type)
	{
		return await _repository.GetByTypeAsync(type);
	}

	public async Task<OrderInfo?> GetByIdAsync(int id)
	{
		return await _repository.GetByIdAsync(id);
	}

	public async Task<IEnumerable<OrderInfo>> GetPageAsync(
		int pageNumber, 
		int pageSize = 25,
		OrderType type = OrderType.CarWash)
	{
		return await _repository.GetSliceAsync(pageNumber, pageSize, type);
	}

	public async Task<int> GetTotalCountAsync(OrderType type)
	{
		return await _repository.GetTotalCount(type);
	}

    public async Task AddAsync(OrderInfo order)
	{
		_repository.Create(order);

		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(OrderInfo order)
	{
		await _repository.UpdateAsync(order);
    }

	public async Task DeleteAsync(int id)
	{
		_repository.Delete(id);

		await _context.SaveChangesAsync();
	}
}
