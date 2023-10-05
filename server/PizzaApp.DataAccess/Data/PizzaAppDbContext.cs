using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Domain.Entities;

namespace PizzaApp.DataAccess.Data
{
    public class PizzaAppDbContext : IdentityDbContext<User>
    {
        public PizzaAppDbContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure column type for Price property
            modelBuilder.Entity<Pizza>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Order>()
                .Property(p => p.OrderPrice)
                .HasColumnType("decimal(18, 2)");
        }
    }
}