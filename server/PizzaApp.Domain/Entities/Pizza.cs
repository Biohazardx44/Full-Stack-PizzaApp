using PizzaApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaApp.Domain.Entities
{
    public class Pizza : BaseEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; } = string.Empty;
        public List<Ingridients> Ingridients { get; set; } = new List<Ingridients>();
        public int? OrderId { get; set; }
        public Order? Order { get; set; }
    }
}