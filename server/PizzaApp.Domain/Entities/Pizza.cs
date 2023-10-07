using PizzaApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PizzaApp.Domain.Entities
{
    public class Pizza : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(250)]
        public string? Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public List<Ingridients> Ingridients { get; set; } = new List<Ingridients>();
        [ForeignKey("User")]
        public string UserId { get; set; } = string.Empty;
        public int? OrderId { get; set; }
        [JsonIgnore]
        public Order? Order { get; set; }
    }
}