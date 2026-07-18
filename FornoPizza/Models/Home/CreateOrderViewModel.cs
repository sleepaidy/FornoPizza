using FornoPizza.Data.Enums;

namespace FornoPizza.Models.Home
{
    public class CreateOrderViewModel
    {
        public int PizzaId { get; set; }
        public Size Size { get; set; }
        public Dough Dough { get; set; }
        public List<int> ToppingIds { get; set; } = new();
        public int Quantity { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string ClientPhone { get; set; } = string.Empty;
        public string ClientAddress { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public PaymentMethod PaymentMethod { get; set; }

    }
}
