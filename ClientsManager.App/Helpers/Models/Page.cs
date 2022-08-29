using ClientsManager.Domain.Enums;

namespace ClientsManager.App.Helpers.Models;

public class Page
{
    public int Number { get; set; }
    public OrderType Type { get; set; }
    public int PageSize { get; set; }
}
