using FornoPizza.Data.Enums;
using FornoPizza.Services;

namespace FornoPizza.Tests
{
    public class OrderStatusServiceTest
    {
        private OrderStatusService _orderStatusService;

        [SetUp]
        public void Setup()
        {
            // CanTransition и GetAllowedNext не обращаются к репозиторию и хабу.
            _orderStatusService = new OrderStatusService(null!, null!);
        }

        [Test]
        public void CanTransition_FromNew_AllowsConfirmedAndCanceledOnly()
        {
            Assert.That(_orderStatusService.CanTransition(OrderStatus.New, OrderStatus.Confirmed), Is.True);
            Assert.That(_orderStatusService.CanTransition(OrderStatus.New, OrderStatus.Canceled), Is.True);
            Assert.That(_orderStatusService.CanTransition(OrderStatus.New, OrderStatus.Cooking), Is.False);
        }

        [Test]
        public void CanTransition_SameStatus_IsForbidden()
        {
            Assert.That(_orderStatusService.CanTransition(OrderStatus.New, OrderStatus.New), Is.False);
            Assert.That(_orderStatusService.CanTransition(OrderStatus.Cooking, OrderStatus.Cooking), Is.False);
        }

        [Test]
        public void CanTransition_FromOnTheWay_AllowsOnlyDelivered()
        {
            Assert.That(_orderStatusService.CanTransition(OrderStatus.OnTheWay, OrderStatus.Delivered), Is.True);
            Assert.That(_orderStatusService.CanTransition(OrderStatus.OnTheWay, OrderStatus.Canceled), Is.False);
            Assert.That(_orderStatusService.CanTransition(OrderStatus.OnTheWay, OrderStatus.Cooking), Is.False);
        }

        [Test]
        public void GetAllowedNext_FromDeliveredAndCanceled_IsEmpty()
        {
            Assert.That(_orderStatusService.GetAllowedNext(OrderStatus.Delivered), Is.Empty);
            Assert.That(_orderStatusService.GetAllowedNext(OrderStatus.Canceled), Is.Empty);
        }
    }
}
