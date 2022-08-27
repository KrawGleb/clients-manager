using ClientsManager.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ClientsManager.Domain.Models
{
    public class OrderInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string? AdditionalName { get; set; }

        [Phone]
        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [MaxLength(30)]
        public string CarModel { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public decimal Price { get; set; }

        [Required]
        public OrderType OrderType { get; set; }
    }
}
