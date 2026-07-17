using FornoPizza.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FornoPizza.Data
{
    public class WebContext : DbContext
    {
        public DbSet<ClientData> Clients { get; set; }
        public DbSet<OrderData> Orders { get; set; }
        public DbSet<PizzaData> Pizzas { get; set; }
        public DbSet<ToppingData> Toppings { get; set; }
        public DbSet<OrderToppingData> OrderToppings { get; set; }

        public WebContext(DbContextOptions<WebContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PizzaData>(entity =>
            {
                entity.Property(x => x.Price)
                .HasPrecision(18, 2);
            });

            modelBuilder.Entity<ToppingData>(entity =>
            {
                entity.Property(x => x.Price)
                .HasPrecision(18, 2);
            });

            modelBuilder.Entity<OrderToppingData>(entity =>
            {
                entity.Property(x => x.ToppingPrice)
                .HasPrecision(18, 2);
            });

            modelBuilder.Entity<OrderData>(entity =>
            {
                entity.Property(x => x.FinalPrice)
                .HasPrecision(18, 2);

                entity.Property(x => x.PizzaPriceInOrder)
                .HasPrecision(18, 2);
            });
        }
    }
}
