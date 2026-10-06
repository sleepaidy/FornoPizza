using FornoPizza.Data;
using FornoPizza.Data.Enums;
using FornoPizza.Data.Models;
using FornoPizza.Data.Repository;
using FornoPizza.Services;
using FornoPizza.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FornoPizza.Tests
{
    public class OrderServiceTest
    {
        [Test]
        public void GetMyOrders_WhenGuest_ReturnsEmpty()
        {
            using var context = CreateContext();
            context.Orders.Add(CreateOrder(userId: 5, date: new DateTime(2026, 10, 1)));
            context.SaveChanges();

            var service = CreateService(context, userId: null);

            var orders = service.GetMyOrders();

            Assert.That(orders, Is.Empty);
        }

        [Test]
        public void GetMyOrders_WhenUserIdIsNotPositive_ReturnsEmpty()
        {
            using var context = CreateContext();
            context.Orders.Add(CreateOrder(userId: 0, date: new DateTime(2026, 10, 1)));
            context.SaveChanges();

            var service = CreateService(context, userId: 0);

            var orders = service.GetMyOrders();

            Assert.That(orders, Is.Empty);
        }

        [Test]
        public void GetMyOrders_ReturnsOnlyCurrentUsersOrders_NewestFirst()
        {
            using var context = CreateContext();
            var older = CreateOrder(userId: 5, date: new DateTime(2026, 10, 1), status: OrderStatus.Canceled);
            var newer = CreateOrder(userId: 5, date: new DateTime(2026, 10, 6), status: OrderStatus.Delivered);
            var someoneElse = CreateOrder(userId: 8, date: new DateTime(2026, 10, 7));
            context.Orders.AddRange(older, newer, someoneElse);
            context.SaveChanges();

            var service = CreateService(context, userId: 5);

            var orders = service.GetMyOrders();

            Assert.That(orders.Select(order => order.Id), Is.EqualTo(new[] { newer.Id, older.Id }));
        }

        private static WebContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<WebContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new WebContext(options);
        }

        private static OrderService CreateService(WebContext context, int? userId)
        {
            return new OrderService(null!, new OrderRepository(context), new FixedUserAuth(userId), new UserRepository(context));
        }

        private static OrderData CreateOrder(int userId, DateTime date, OrderStatus status = OrderStatus.New)
        {
            return new OrderData
            {
                UserId = userId,
                DateOfOrder = date,
                Status = status,
                ClientId = 1,
                FinalPrice = 610
            };
        }

        private sealed class FixedUserAuth : IAuthService
        {
            private readonly int? _userId;

            public FixedUserAuth(int? userId)
            {
                _userId = userId;
            }

            public int? GetCurrentUserId() => _userId;

            public Task SignInAsync(UserData user) => Task.CompletedTask;
        }
    }
}
