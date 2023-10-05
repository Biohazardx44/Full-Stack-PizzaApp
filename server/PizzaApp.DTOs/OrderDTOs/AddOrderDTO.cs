using PizzaApp.Domain.Entities;

namespace PizzaApp.DTOs.OrderDTOs
{
    public class AddOrderDTO
    {
        public string UserId { get; set; } = string.Empty;
        public string AddressTo { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal OrderPrice { get; set; }
        public List<Pizza>? Pizzas { get; set; } = new List<Pizza>();
    }
}