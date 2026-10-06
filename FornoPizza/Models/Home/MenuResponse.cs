namespace FornoPizza.Models.Home
{
    public class MenuResponse
    {
        public List<PizzaMenuItem> Pizzas { get; set; } = new();
        public List<ToppingMenuItem> Toppings { get; set; } = new();
    }
}
