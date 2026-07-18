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

                entity.HasData(
                    new PizzaData
                    {
                        Id = 1,
                        Name = "Маргарита",
                        Ingredients = "Томаты, моцарелла, базилик",
                        ImageUrl = "https://images.unsplash.com/photo-1574071318508-1cdbab80d002?auto=format&fit=crop&w=800&q=80",
                        Price = 490,
                        IsAvailable = true
                    },
                    new PizzaData
                    {
                        Id = 2,
                        Name = "Пепперони",
                        Ingredients = "Острая колбаса, сыр, томатный соус",
                        ImageUrl = "https://images.unsplash.com/photo-1628840042765-356cda07504e?auto=format&fit=crop&w=800&q=80",
                        Price = 590,
                        IsAvailable = true
                    },
                    new PizzaData
                    {
                        Id = 3,
                        Name = "Грибная",
                        Ingredients = "Шампиньоны, сливки, трюфельное масло",
                        ImageUrl = "https://images.unsplash.com/photo-1593560708920-61dd98c46a4e?auto=format&fit=crop&w=800&q=80",
                        Price = 620,
                        IsAvailable = true
                    },
                    new PizzaData
                    {
                        Id = 4,
                        Name = "Четыре сыра",
                        Ingredients = "Моцарелла, горгонзола, пармезан, чеддер",
                        ImageUrl = "https://images.unsplash.com/photo-1548365328-9f68587ace8e?auto=format&fit=crop&w=800&q=80",
                        Price = 650,
                        IsAvailable = true
                    });
            });

            modelBuilder.Entity<ToppingData>(entity =>
            {
                entity.Property(x => x.Price)
                .HasPrecision(18, 2);

                entity.HasData(
                    new ToppingData
                    {
                        Id = 1,
                        Name = "Моцарелла",
                        Price = 70
                    },
                    new ToppingData
                    {
                        Id = 2,
                        Name = "Бекон",
                        Price = 90
                    },
                    new ToppingData
                    {
                        Id = 3,
                        Name = "Халапеньо",
                        Price = 50
                    },
                    new ToppingData
                    {
                        Id = 4,
                        Name = "Оливки",
                        Price = 60
                    },
                    new ToppingData
                    {
                        Id = 5,
                        Name = "Грибы",
                        Price = 70
                    },
                    new ToppingData
                    {
                        Id = 6,
                        Name = "Соус барбекю",
                        Price = 40
                    });
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
