using FornoPizza.Data.Models;

namespace FornoPizza.Models.Home
{
    public class IndexViewModel
    {
        public List<PizzaData> Pizzas { get; set; } = new();
        public List<ToppingData> Toppings { get; set; } = new();
        public CreateOrderViewModel CreateOrder { get; set; } = new();
    }
}
