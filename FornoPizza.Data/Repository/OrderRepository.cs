using FornoPizza.Data.Models;
using FornoPizza.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using FornoPizza.Data.Enums;

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

        public OrderData? GetById(int id)
        {
            return _webContext.Orders.FirstOrDefault(x => x.Id == id);
        }

        public void Update(OrderData orderData)
        {
            if (orderData is null)
            {
                throw new ArgumentNullException(nameof(orderData));
            }
            _webContext.SaveChanges();
        }

        public List<OrderData> GetActiveOrders()
        {
            var orders = _webContext.Orders;

            return orders
                .Include(x => x.Client)
                .Where(x => x.Status != OrderStatus.Delivered && x.Status != OrderStatus.Canceled)
                .OrderBy(x => x.DateOfOrder)
                .ToList();
        }

        public List<OrderData> GetByUserId(int userId)
        {
            if (userId <= 0)
            {
                return new List<OrderData>();
            }
            var orders = _webContext.Orders;
            return orders
                .Include(x => x.OrderItems)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.DateOfOrder)
                .ToList();
        }
    }
}
