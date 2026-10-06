using FornoPizza.Data.Enums;
using FornoPizza.Data.Repository.Interfaces;
using FornoPizza.Services.Interfaces;

namespace FornoPizza.Services
{
    public class OrderQueueBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<OrderQueueBackgroundService> _logger;

        public OrderQueueBackgroundService(IServiceScopeFactory serviceScopeFactory, ILogger<OrderQueueBackgroundService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
                    var orders = orderRepository.GetActiveOrders();
                    _logger.LogInformation("In queue: {Count}", orders.Count());

                    var orderStatusService = scope.ServiceProvider.GetRequiredService<IOrderStatusService>();
                    var deadline = DateTime.UtcNow - TimeSpan.FromMinutes(20);
                    var deadlineOrders = orders
                        .Where(x => x.Status == OrderStatus.New && x.DateOfOrder < deadline);
                    foreach ( var order in deadlineOrders )
                    {
                        try
                        {
                             await orderStatusService.ChangeStatus(order.Id, OrderStatus.Canceled);
                            _logger.LogInformation("Заказ {OrderId} отменён: кухня не подтвердила его за 20 минут", order.Id);
                        }
                        catch(InvalidOperationException ex)
                        {
                            _logger.LogError(ex, "Не удалось отменить заказ {OrderId}", order.Id);
                        }

                    }
                }
                catch (Exception ex)
                {

                    _logger.LogError(ex, "Не удалось прочитать очередь кухни");
                }
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
