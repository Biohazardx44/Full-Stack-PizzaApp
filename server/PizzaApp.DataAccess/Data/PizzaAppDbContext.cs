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

            // PIZZA

            // Validations
            modelBuilder.Entity<Pizza>()
                .Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Pizza>()
                .Property(x => x.Description)
                .HasMaxLength(250);

            modelBuilder.Entity<Pizza>()
                .Property(x => x.Price)
                .IsRequired();

            modelBuilder.Entity<Pizza>()
                .Property(x => x.Ingridients)
                .IsRequired();

            // ORDER

            // Validations
            modelBuilder.Entity<Order>()
                .Property(x => x.AddressTo)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .Property(x => x.Description)
                .HasMaxLength(250);

            modelBuilder.Entity<Order>()
                .Property(x => x.OrderPrice)
                .IsRequired();

            // Relations
            modelBuilder.Entity<Order>()
                .HasMany(x => x.Pizzas)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // USER

            // Validations
            modelBuilder.Entity<User>()
                .Property(x => x.UserName)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(x => x.Email)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(x => x.PasswordHash)
                .IsRequired();

            // Configure column type for Price property
            modelBuilder.Entity<Pizza>()
                .Property(x => x.Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Order>()
                .Property(x => x.OrderPrice)
                .HasColumnType("decimal(18, 2)");
        }
    }
}