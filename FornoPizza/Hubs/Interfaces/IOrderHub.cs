namespace FornoPizza.Hubs.Interfaces
{
    public interface IOrderHub
    {
        public Task OrderStatusChanged(int  orderId, string status);
    }
}
