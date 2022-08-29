using ClientsManager.Application.Services.Interfaces;
using ClientsManager.Domain.Enums;
using ClientsManager.Domain.Models;
using ClientsManager.Infrastructure.Persistence;
using ClientsManager.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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

	public async Task<OrderInfo?> GetByIdAsync(int id)
	{
		return await _repository.GetByIdAsync(id);
	}

    public async Task AddAsync(OrderInfo order)
	{
		_repository.Create(order);

		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(OrderInfo order)
	{
		var oldEntry = await _repository.GetByIdAsync(order.Id);

		if (oldEntry is null)
		{
			return;
		}

		oldEntry.FirstName = order.FirstName;
		oldEntry.LastName = order.LastName;
		oldEntry.AdditionalName = order.AdditionalName;
		oldEntry.PhoneNumber = order.PhoneNumber;
		oldEntry.Description = order.Description;
		oldEntry.CarModel = order.CarModel;
		oldEntry.CarNumber = order.CarNumber;
		oldEntry.OrderType = order.OrderType;
		oldEntry.Price = order.Price;

		_context.Entry(oldEntry).State = EntityState.Modified;

		await _context.SaveChangesAsync();
    }
}
