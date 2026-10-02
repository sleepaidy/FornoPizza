using FornoPizza.Data.Enums;
using FornoPizza.Data.Repository.Interfaces;
using FornoPizza.Hubs;
using FornoPizza.Hubs.Interfaces;
using FornoPizza.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace FornoPizza.Services
{
    public class OrderStatusService : IOrderStatusService
    {

        private readonly IOrderRepository _orderRepository;
        private readonly IHubContext<OrderHub, IOrderHub> _hubContext;

        public OrderStatusService(IOrderRepository orderRepository, IHubContext<OrderHub, IOrderHub> hubContext)
        {
            _orderRepository = orderRepository;
            _hubContext = hubContext;
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

        public async Task ChangeStatus(int orderId, OrderStatus next)
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
            if (order.UserId is int userId)
            {
                await _hubContext.Clients.User(userId.ToString())
                    .OrderStatusChanged(order.Id, order.Status.ToString());
            }
        }
    }
}
