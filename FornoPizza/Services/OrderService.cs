using FornoPizza.Data.Models;
using FornoPizza.Data.Repository.Interfaces;
using FornoPizza.Models.Dtos;
using FornoPizza.Models.Home;
using FornoPizza.Services.Interfaces;

namespace FornoPizza.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPricingService _pricingService;
        private readonly IAuthService _authService;

        public OrderService(IPricingService pricingService, IOrderRepository orderRepository, IAuthService authService)
        {
            _pricingService = pricingService;
            _orderRepository = orderRepository;
            _authService = authService;
        }

        public int CreateOrder(CreateOrderViewModel createOrderViewModel)
        {
            if (createOrderViewModel is null)
            {
                throw new ArgumentNullException(nameof(createOrderViewModel));
            }
            if (createOrderViewModel.OrderItems is null || createOrderViewModel.OrderItems.Count == 0)
            {
                throw new ArgumentException(nameof(createOrderViewModel.OrderItems));
            }

            var lines = new List<OrderLineDto>();

            foreach (var item in createOrderViewModel.OrderItems)
            {
                var line = _pricingService.CalculateOnePosition(
                    item.PizzaId,
                    item.Size,
                    item.Dough,
                    item.ToppingIds,
                    item.Quantity);
                lines.Add(line);
            }

            var orderTotal = _pricingService.CalculateTotalOrder(lines);

            var client = new ClientData()
            {
                Name = createOrderViewModel.ClientName,
                TelephoneNumber = createOrderViewModel.ClientPhone,
                Address = createOrderViewModel.ClientAddress
            };

            var orderData = new OrderData()
            {
                FinalPrice = orderTotal,
                PaymentMethod = createOrderViewModel.PaymentMethod,
                Comment = createOrderViewModel.Comment,
                Client = client,
                UserId = _authService.GetCurrentUserId()
            };

            for (int i = 0; i < lines.Count; i++)
            {
                var item = createOrderViewModel.OrderItems[i];
                var line = lines[i];

                var orderItem = new OrderItemData()
                {
                    Size = item.Size,
                    Dough = item.Dough,
                    Quantity = item.Quantity,
                    LinePrice = line.LinePrice,
                    PizzaId = item.PizzaId,
                    PizzaNameInOrder = line.OrderPizza.PizzaName,
                    PizzaPriceInOrder = line.OrderPizza.PizzaPrice
                };
                orderData.OrderItems.Add(orderItem);
                foreach (var t in line.OrderToppings)
                {
                    var orderTopping = new OrderToppingData
                    {
                        ToppingId = t.ToppingId,
                        ToppingName = t.ToppingName,
                        ToppingPrice = t.ToppingPrice
                    };
                    orderItem.OrderToppings.Add(orderTopping);
                }
            }

            _orderRepository.CreateOrder(orderData);
            return orderData.Id;
        }

        public List<OrderData> GetMyOrders()
        {
            if (_authService.GetCurrentUserId() == null)
            {
                return new List<OrderData>();
            }
            var userId = _authService.GetCurrentUserId();

            return _orderRepository.GetByUserId(userId.Value);
        }
    }
}
