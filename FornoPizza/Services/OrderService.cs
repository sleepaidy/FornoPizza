using FornoPizza.Data.Models;
using FornoPizza.Data.Repository.Interfaces;
using FornoPizza.Localization;
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
        private readonly IUserRepository _userRepository;

        public OrderService(
            IPricingService pricingService,
            IOrderRepository orderRepository,
            IAuthService authService,
            IUserRepository userRepository)
        {
            _pricingService = pricingService;
            _orderRepository = orderRepository;
            _authService = authService;
            _userRepository = userRepository;
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

            var clientName = createOrderViewModel.ClientName?.Trim() ?? string.Empty;
            var clientPhone = createOrderViewModel.ClientPhone?.Trim() ?? string.Empty;
            var clientAddress = createOrderViewModel.ClientAddress?.Trim() ?? string.Empty;
            if (clientName.Length == 0 || clientPhone.Length == 0 || clientAddress.Length == 0)
            {
                throw new InvalidOperationException(Shared.Error_Customer);
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

            var promoCode = string.IsNullOrWhiteSpace(createOrderViewModel.PromoCode)
                ? null
                : createOrderViewModel.PromoCode.Trim();
            var discount = _pricingService.CalculatePromoDiscount(promoCode ?? string.Empty, orderTotal);

            var client = new ClientData()
            {
                Name = clientName,
                TelephoneNumber = clientPhone,
                Address = clientAddress
            };

            var orderData = new OrderData()
            {
                FinalPrice = orderTotal - discount,
                DiscountValue = discount,
                Code = promoCode,
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
            if (orderData.UserId is int userId && userId > 0)
            {
                _userRepository.RememberAddress(userId, clientAddress);
            }

            return orderData.Id;
        }

        public List<OrderData> GetMyOrders()
        {
            var userId = _authService.GetCurrentUserId();

            if (userId is null)
            {
                return new List<OrderData>();
            }

            return _orderRepository.GetByUserId(userId.Value);
        }
        public PromoPreviewResult PreviewPromo(string promoCode, List<OrderItemViewModel> orderItems)
        {
            if (orderItems is null || orderItems.Count == 0)
            {
                throw new ArgumentException(nameof(orderItems));
            }

            var lines = new List<OrderLineDto>();
            foreach (var item in orderItems)
            {
                lines.Add(_pricingService.CalculateOnePosition(
                    item.PizzaId,
                    item.Size,
                    item.Dough,
                    item.ToppingIds,
                    item.Quantity));
            }

            var orderTotal = _pricingService.CalculateTotalOrder(lines);
            var discount = _pricingService.CalculatePromoDiscount(promoCode ?? string.Empty, orderTotal);

            return new PromoPreviewResult
            {
                Discount = discount,
                FinalPrice = orderTotal - discount
            };
        }
    }
}
