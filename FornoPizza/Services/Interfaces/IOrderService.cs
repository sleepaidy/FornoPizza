using FornoPizza.Data.Models;
using FornoPizza.Models.Home;

namespace FornoPizza.Services.Interfaces
{
    public interface IOrderService
    {
        public int CreateOrder(CreateOrderViewModel createOrderViewModel);
        public List<OrderData> GetMyOrders();
        public PromoPreviewResult PreviewPromo(string promoCode, List<OrderItemViewModel> orderItems);
    }
}
