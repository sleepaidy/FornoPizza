using FornoPizza.Data.Enums;

namespace FornoPizza.Models.Home
{
    public class CreateOrderViewModel
    {
        public string ClientName { get; set; } = string.Empty;
        public string ClientPhone { get; set; } = string.Empty;
        public string ClientAddress { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public PaymentMethod PaymentMethod { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; } = new();

    }
}
