using ClientsManager.Domain.Models;
using ClientsManager.Infrastructure.Persistence.Builders.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ClientsManager.Infrastructure.Persistence.Builders;

public class OrdersTableQueryBuilder : IOrdersTableQueryBuilder
{
	private readonly ApplicationDbContext _context;

	public OrdersTableQueryBuilder(ApplicationDbContext context)
	{
		_context = context;

		Query = context.Set<OrderInfo>();
	}

    public IQueryable<OrderInfo> Query { get; set; }


    public IOrdersTableQueryBuilder AddFilter(Expression<Func<OrderInfo, bool>> filter)
	{
		Query = Query.Where(filter);

		return this;
	}

	public IOrdersTableQueryBuilder ClearQuery()
	{
		Query = _context.Set<OrderInfo>();

		return this;
	}

	public async Task<IEnumerable<OrderInfo>> BuildAsync()
		=> await Query.ToListAsync();
}
