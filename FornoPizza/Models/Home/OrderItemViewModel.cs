using FornoPizza.Data.Enums;

namespace FornoPizza.Models.Home
{
    public class OrderItemViewModel
    {
        public int PizzaId { get; set; }
        public Size Size { get; set; }
        public Dough Dough { get; set; }
        public List<int> ToppingIds { get; set; } = new();
        public int Quantity { get; set; }
    }
}
