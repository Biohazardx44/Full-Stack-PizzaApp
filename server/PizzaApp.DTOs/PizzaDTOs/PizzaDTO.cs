using PizzaApp.Domain.Enums;

namespace PizzaApp.DTOs.PizzaDTOs
{
    public class PizzaDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; } = string.Empty;
        public List<Ingridients> Ingridients { get; set; } = new List<Ingridients>();
        public int? OrderId { get; set; }
    }
}