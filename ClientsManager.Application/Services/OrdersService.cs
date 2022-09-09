using ClientsManager.Application.Services.Interfaces;
using ClientsManager.Domain.Enums;
using ClientsManager.Domain.Models;
using ClientsManager.Infrastructure.Persistence;
using ClientsManager.Infrastructure.Persistence.Builders.Extenstions.OrdersTableQueryBuilderExtensions;
using ClientsManager.Infrastructure.Persistence.Builders.Interfaces;
using ClientsManager.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClientsManager.Application.Services;

public class OrdersService : IOrdersService
{
	private readonly ApplicationDbContext _context;
	private readonly IOrdersRepository _repository;
	private readonly ISearchingService _searchingService;
	private readonly IOrdersTableQueryBuilder _ordersTableQueryBuilder;

	public OrdersService(
		ApplicationDbContext context,
		IOrdersRepository repository,
		ISearchingService searchingService,
		IOrdersTableQueryBuilder ordersTableQueryBuilder)
	{
		_context = context;
		_repository = repository;
		_searchingService = searchingService;
		_ordersTableQueryBuilder = ordersTableQueryBuilder;
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
		OrderType type = OrderType.CarWash,
		SearchOptions searchOption = SearchOptions.None,
		string searchParameter = "",
		string sortBy = "",
		string sortOrder = "")
	{
		_ordersTableQueryBuilder
			.ClearQuery()
			.WithType(type)
			.WithSearching(searchOption, searchParameter)
			.OrderBy(sortBy, sortOrder)
			.WithPagination(pageNumber, pageSize);

		var list = await _ordersTableQueryBuilder.BuildAsync();
		return list;
	}

	public int GetTotalCountAsync(
		OrderType type,
		SearchOptions searchOption,
		string? searchParameter)
	{
		return _ordersTableQueryBuilder
			.ClearQuery()
			.WithType(type)
			.WithSearching(searchOption, searchParameter)
			.Count();
    }

    public void Add(OrderInfo order)
	{
		_repository.Create(order);
	}

	public async Task AddRangeAsync(IEnumerable<OrderInfo> orders)
	{
		await _repository.AddRangeAsync(orders);
	}

	public async Task UpdateAsync(OrderInfo order)
	{
		await _repository.UpdateAsync(order);
    }

	public void Delete(int id)
	{
		_repository.DeleteAsync(id);
	}

	public async Task ClearAsync()
	{
		await _repository.ClearAsync();
	}
}
