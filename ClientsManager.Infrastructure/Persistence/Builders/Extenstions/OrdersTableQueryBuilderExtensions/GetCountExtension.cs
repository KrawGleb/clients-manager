using ClientsManager.Infrastructure.Persistence.Builders.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsManager.Infrastructure.Persistence.Builders.Extenstions.OrdersTableQueryBuilderExtensions;

public static class GetCountExtension
{
    public static int Count(this IOrdersTableQueryBuilder builder)
    {
        return builder.Query.Count();
    }
}
