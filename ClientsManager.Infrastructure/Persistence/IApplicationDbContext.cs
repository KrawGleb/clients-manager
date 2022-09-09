using ClientsManager.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientsManager.Infrastructure.Persistence;

public interface IApplicationDbContext
{
    DbSet<OrderInfo> Orders { get; set; }
}