using ClientsManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsManager.App.Models;

public class OrderInfoModel
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string CarModel { get; set; }
    public string CarNumber { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public OrderType OrderType { get; set; }
}
