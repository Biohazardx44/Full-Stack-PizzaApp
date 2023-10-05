using PizzaApp.Domain.Entities;
using System.Text.Json.Serialization;

namespace PizzaApp.DTOs.OrderDTOs
{
    public class UpdateOrderDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public string UserId { get; set; } = string.Empty;
        public string AddressTo { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal OrderPrice { get; set; }
        public List<Pizza>? Pizzas { get; set; } = new List<Pizza>();
    }
}