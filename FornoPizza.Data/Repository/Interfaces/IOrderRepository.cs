using FornoPizza.Data.Models;

namespace FornoPizza.Data.Repository.Interfaces
{
    public interface IOrderRepository
    {
        public void CreateOrder(OrderData orderData); 
    }
}
