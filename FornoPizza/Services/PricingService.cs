using FornoPizza.Data;
using FornoPizza.Data.Enums;
using FornoPizza.Data.Models;
using FornoPizza.Models.Dtos;
using FornoPizza.Services.Interfaces;

namespace FornoPizza.Services
{
    public class PricingService : IPricingService
    {

        private readonly WebContext _webContext;

        private const decimal SMALL_SIZE = 0;
        private const decimal MEDIUM_SIZE = 120;
        private const decimal BIG_SIZE = 220;

        private const decimal DOUGH_THIN = 0;
        private const decimal DOUGH_CLASSIC = 0;
        private const decimal DOUGH_CHEESE_FILLED_CRUST = 80;
        public PricingService(WebContext webContext)
        {
            _webContext = webContext;
        }

        private decimal GetSizePrice(Size size)
        {
            return size switch
            {
                Size.Small => SMALL_SIZE,
                Size.Medium => MEDIUM_SIZE,
                Size.Big => BIG_SIZE,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private decimal GetDoughPrice(Dough dough)
        {
            return dough switch
            {
                Dough.Thin => DOUGH_THIN,
                Dough.Classic => DOUGH_CLASSIC,
                Dough.CheeseFilledCrust => DOUGH_CHEESE_FILLED_CRUST,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private decimal GetToppingPriceSum(IEnumerable<ToppingData> toppings)
        {
            if(toppings == null)
            {
                return 0;
            }
            return toppings.Sum(x => x.Price);
        }
        public FinalOrderDto Calculate(int pizzaId, Size size, Dough dough, IEnumerable<int> toppingIds, int quantity)
        {
            toppingIds = toppingIds ?? Array.Empty<int>();
            if(quantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity));
            }

            var pizza = _webContext.Pizzas.FirstOrDefault(p => p.Id == pizzaId);
            if (pizza == null || pizza.IsAvailable == false)
            {
                throw new InvalidOperationException("Пицца не найдена или недоступна.");
            }
            var toppings = _webContext.Toppings.Where(x => toppingIds.Contains(x.Id) && x.IsAvailable == true).ToList();
            var sizePrice = GetSizePrice(size);
            var doughPrice = GetDoughPrice(dough);

            var finalPrice = (pizza.Price
                + sizePrice
                + doughPrice
                + GetToppingPriceSum(toppings))
                * quantity;

            var orderPizza = new OrderPizzaDto
            {
                PizzaId = pizza.Id,
                PizzaName = pizza.Name,
                PizzaPrice = pizza.Price,
            };

            var orderToppings = toppings.Select(x => new OrderToppingDto
            {
                ToppingId = x.Id,
                ToppingName = x.Name,
                ToppingPrice = x.Price,
            }).ToList();
            

            return new FinalOrderDto
            {
                FinalPrice = finalPrice,
                Quantity = quantity,
                OrderPizza = orderPizza,
                OrderToppings = orderToppings,
            };
        }
    }
}
