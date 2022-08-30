using ClientsManager.Domain.Models;
using System.Linq.Expressions;

namespace ClientsManager.Infrastructure.Persistence.Builders.Interfaces;
public interface IOrdersTableQueryBuilder
{
    IOrdersTableQueryBuilder AddFilter(Expression<Func<OrderInfo, bool>> filter);
    Task<IEnumerable<OrderInfo>> BuildAsync();
    IOrdersTableQueryBuilder ClearQuery();

    IQueryable<OrderInfo> Query { get; set; }
}