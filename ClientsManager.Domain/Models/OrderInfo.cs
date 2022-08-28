using ClientsManager.Domain.Enums;
using ClientsManager.Domain.Models.Interfaces;

namespace ClientsManager.Domain.Models
{
    public class OrderInfo : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? AdditionalName { get; set; }
        public string PhoneNumber { get; set; }
        public string CarModel { get; set; }
        public string CarNumber { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public OrderType OrderType { get; set; }
    }
}
