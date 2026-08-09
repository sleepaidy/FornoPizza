using FornoPizza.Data.Models;
using FornoPizza.Data.Repository.Interfaces;

namespace FornoPizza.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly WebContext _webContext;

        public OrderRepository(WebContext webContext)
        {
            _webContext = webContext;
        }

        public void CreateOrder(OrderData orderData)
        {
            if (orderData is null)
            {
                throw new ArgumentNullException(nameof(orderData));
            }

           _webContext.Orders.Add(orderData);
            _webContext.SaveChanges();
        }
    }
}
