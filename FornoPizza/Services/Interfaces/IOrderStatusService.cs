using FornoPizza.Data.Enums;

namespace FornoPizza.Services.Interfaces
{
    public interface IOrderStatusService
    {
        public bool CanTransition(OrderStatus current, OrderStatus next);
        public IReadOnlyList<OrderStatus> GetAllowedNext(OrderStatus current);
        public void ChangeStatus(int orderId, OrderStatus next);
    }
}
