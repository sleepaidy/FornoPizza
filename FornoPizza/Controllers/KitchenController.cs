using FornoPizza.Data.Enums;
using FornoPizza.Data.Repository.Interfaces;
using FornoPizza.Models.Kitchen;
using FornoPizza.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FornoPizza.Controllers
{
    [Authorize(Roles = "Kitchen")]
    public class KitchenController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderStatusService _orderStatusService;

        public KitchenController(IOrderRepository orderRepository, IOrderStatusService orderStatusService)
        {
            _orderRepository = orderRepository;
            _orderStatusService = orderStatusService;
        }

        public IActionResult Index()
        {
            var cards = _orderRepository.GetActiveOrders()
                .Select(order =>
                {
                    var allowed = _orderStatusService.GetAllowedNext(order.Status);
                    var next = allowed.Where(status => status != OrderStatus.Canceled).ToList();
                    return new KitchenOrderViewModel
                    {
                        Order = order,
                        NextStatus = next.Count == 0 ? null : next[0],
                        CanCancel = allowed.Contains(OrderStatus.Canceled)
                    };
                })
                .ToList();

            return View(cards);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Advance(int id)
        {
            try
            {
                var order = _orderRepository.GetById(id);
                if (order is null)
                {
                    throw new InvalidOperationException("Заказ не найден.");
                }

                var nextCandidates = _orderStatusService.GetAllowedNext(order.Status)
                    .Where(status => status != OrderStatus.Canceled)
                    .ToList();

                if (nextCandidates.Count == 0)
                {
                    throw new InvalidOperationException("Нет следующего статуса.");
                }

                _orderStatusService.ChangeStatus(id, nextCandidates[0]);
            }
            catch (InvalidOperationException)
            {
                TempData["KitchenError"] = "Не удалось сменить статус заказа.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cancel(int id)
        {
            try
            {
                _orderStatusService.ChangeStatus(id, OrderStatus.Canceled);
            }
            catch (InvalidOperationException)
            {
                TempData["KitchenError"] = "Этот заказ нельзя отменить.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
