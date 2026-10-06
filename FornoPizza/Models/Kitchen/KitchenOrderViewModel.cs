using FornoPizza.Data.Enums;
using FornoPizza.Data.Models;

namespace FornoPizza.Models.Kitchen
{
    public class KitchenOrderViewModel
    {
        public OrderData Order { get; set; } = null!;
        public OrderStatus? NextStatus { get; set; }
        public bool CanCancel { get; set; }
    }
}
