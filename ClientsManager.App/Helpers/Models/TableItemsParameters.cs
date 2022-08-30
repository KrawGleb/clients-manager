using ClientsManager.Domain.Enums;

namespace ClientsManager.App.Helpers.Models;

public class TableItemsParameters
{
    public SearchOptions SearchOption { get; set; }
    public string SearchValue { get; set; }
    public OrderType Tab { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}
