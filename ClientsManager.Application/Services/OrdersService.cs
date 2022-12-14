using ClientsManager.Application.Services.Interfaces;
using ClientsManager.Domain.Enums;
using ClientsManager.Domain.Models;
using ClientsManager.Infrastructure.Persistence.Builders.Extenstions.OrdersTableQueryBuilderExtensions;
using ClientsManager.Infrastructure.Persistence.Builders.Interfaces;
using ClientsManager.Infrastructure.Persistence.Repositories.Interfaces;

namespace ClientsManager.Application.Services;

public class OrdersService : IOrdersService
{
	private readonly IOrdersRepository _repository;
	private readonly IOrdersTableQueryBuilder _ordersTableQueryBuilder;

	public OrdersService(
		IOrdersRepository repository,
		IOrdersTableQueryBuilder ordersTableQueryBuilder)
	{
		_repository = repository;
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

	public int GetTotalCount(
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

    public async Task AddAsync(OrderInfo order)
	{
		await _repository.CreateAsync(order);
	}

	public async Task AddRangeAsync(IEnumerable<OrderInfo> orders)
	{
		await _repository.AddRangeAsync(orders);
	}

	public async Task UpdateAsync(OrderInfo order)
	{
		await _repository.UpdateAsync(order);
    }

	public async Task DeleteAsync(int id)
	{
		await _repository.DeleteAsync(id);
	}

	public async Task ClearAsync()
	{
		await _repository.ClearAsync();
	}
}
