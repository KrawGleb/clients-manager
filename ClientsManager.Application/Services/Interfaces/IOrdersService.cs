using ClientsManager.Domain.Models;

namespace ClientsManager.Application.Services.Interfaces;

public interface IOrdersService
{
    Task AddAsync(OrderInfo order);
}
