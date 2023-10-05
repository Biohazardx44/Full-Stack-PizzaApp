using PizzaApp.Domain.Enums;

namespace PizzaApp.DTOs.PizzaDTOs
{
    public class UpdatePizzaDTO
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public List<Ingridients> Ingridients { get; set; } = new List<Ingridients>();
        public int? OrderId { get; set; }
    }
}