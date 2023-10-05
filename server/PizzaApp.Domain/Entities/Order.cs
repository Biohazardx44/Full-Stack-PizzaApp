﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaApp.Domain.Entities
{
    public class Order : BaseEntity
    {
        [ForeignKey("UserId")]
        public string UserId { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string AddressTo { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal OrderPrice { get; set; }
        public List<Pizza>? Pizzas { get; set; } = new List<Pizza>();
    }
}