using FornoPizza.Data.Repository.Interfaces;
using FornoPizza.Models.Auth;
using FornoPizza.Models.Home;
using FornoPizza.Services.Interfaces;

namespace FornoPizza.Endpoints;

public static class JsonApiEndpoints
{
    public static void MapJsonApi(this WebApplication app)
    {
        app.MapGet("/Home/Menu", (IMenuRepository menuRepository) =>
        {
            var menu = new MenuResponse
            {
                Pizzas = menuRepository.GetPizzaMenu()
                    .Select(pizza => new PizzaMenuItem
                    {
                        Id = pizza.Id,
                        Name = pizza.Name,
                        Ingredients = pizza.Ingredients,
                        ImageUrl = pizza.ImageUrl,
                        Price = pizza.Price
                    })
                    .ToList(),
                Toppings = menuRepository.GetToppingsMenu()
                    .Select(topping => new ToppingMenuItem
                    {
                        Id = topping.Id,
                        Name = topping.Name,
                        Price = topping.Price
                    })
                    .ToList()
            };

            return Results.Ok(menu);
        });

        app.MapPost("/Home/CreateOrderJson", (CreateOrderViewModel viewModel, IOrderService orderService) =>
        {
            try
            {
                var orderId = orderService.CreateOrder(viewModel);
                return Results.Ok(new { OrderId = orderId });
            }
            catch (InvalidOperationException exception)
            {
                return Results.BadRequest(new { message = exception.Message });
            }
        });

        app.MapPost("/Home/PreviewPromo", (CreateOrderViewModel viewModel, IOrderService orderService) =>
        {
            try
            {
                var result = orderService.PreviewPromo(viewModel.PromoCode, viewModel.OrderItems);
                return Results.Ok(result);
            }
            catch (InvalidOperationException exception)
            {
                return Results.BadRequest(new { message = exception.Message });
            }
        });

        app.MapGet("/Account/MyAddresses", (IAuthService authService, IUserRepository userRepository) =>
        {
            var userId = authService.GetCurrentUserId();
            if (userId is null or <= 0)
            {
                return Results.Ok(new List<SavedAddressItem>());
            }

            var items = userRepository.GetAddresses(userId.Value)
                .Select(address => new SavedAddressItem
                {
                    Id = address.Id,
                    Address = address.Address
                })
                .ToList();

            return Results.Ok(items);
        });
    }
}
