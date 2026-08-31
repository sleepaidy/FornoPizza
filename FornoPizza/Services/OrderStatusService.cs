using FornoPizza.Data.Enums;
using FornoPizza.Data.Repository.Interfaces;
using FornoPizza.Services.Interfaces;

namespace FornoPizza.Services
{
    public class OrderStatusService : IOrderStatusService
    {

        private readonly IOrderRepository _orderRepository;

        public OrderStatusService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public bool CanTransition(OrderStatus current, OrderStatus next)
        {
            if (current == next)
            {
                return false;
            }

            return GetAllowedNext(current).Contains(next);
        }

        public IReadOnlyList<OrderStatus> GetAllowedNext(OrderStatus current)
        {
            switch (current)
            {
                case OrderStatus.New:
                    return new[] { OrderStatus.Confirmed, OrderStatus.Canceled };
                case OrderStatus.Confirmed:
                    return new[] { OrderStatus.Cooking, OrderStatus.Canceled };
                case OrderStatus.Cooking:
                    return new[] { OrderStatus.OnTheWay, OrderStatus.Canceled };
                case OrderStatus.OnTheWay:
                    return new[] { OrderStatus.Delivered };
                case OrderStatus.Delivered:
                    return Array.Empty<OrderStatus>();
                case OrderStatus.Canceled:
                    return Array.Empty<OrderStatus>();
                default:
                    throw new ArgumentOutOfRangeException(nameof(current), current, null);
            }

        }

        public void ChangeStatus(int orderId, OrderStatus next)
        {
            if (orderId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(orderId), orderId, null);
            }
            var order = _orderRepository.GetById(orderId);
            if (order == null)
            {
                throw new InvalidOperationException("Заказ не найден.");
            }
            if (!CanTransition(order.Status, next))
            {
                throw new InvalidOperationException("Недопустимый переход статуса.");
            }
            order.Status = next;
            _orderRepository.Update(order);
        }
    }
}
