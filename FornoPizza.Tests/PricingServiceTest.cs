using FornoPizza.Data;
using FornoPizza.Data.Enums;
using FornoPizza.Services;
using Microsoft.EntityFrameworkCore;

namespace FornoPizza.Tests
{
    public class PricingServiceTest
    {
        [Test]
        public void CalculateOnePosition_MargheritaMediumThin_LinePriceIs610()
        {
            var options = new DbContextOptionsBuilder<WebContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new WebContext(options);
            context.Database.EnsureCreated();

            var pricingService = new PricingService(context);

            var line = pricingService.CalculateOnePosition(
                pizzaId: 1,
                size: Size.Medium,
                dough: Dough.Thin,
                toppingIds: Array.Empty<int>(),
                quantity: 1);

            Assert.That(line.LinePrice, Is.EqualTo(610m));
        }
    }
}
