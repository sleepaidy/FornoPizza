using FornoPizza.Models.Home;

namespace FornoPizza.Services.Interfaces
{
    public interface IOrderService
    {
        public int CreateOrder(CreateOrderViewModel createOrderViewModel);
    }
}
