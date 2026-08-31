using FornoPizza.Data.Models;

namespace FornoPizza.Data.Repository.Interfaces
{
    public interface IOrderRepository
    {
        public void CreateOrder(OrderData orderData);
        public void Update(OrderData orderData);
        public OrderData? GetById(int id);
        public List<OrderData> GetActiveOrders();
    }
}
