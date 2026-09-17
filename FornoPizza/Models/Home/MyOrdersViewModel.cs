using FornoPizza.Data.Enums;
using FornoPizza.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace FornoPizza.Models.Home
{
    public class MyOrdersViewModel
    {
        public int Id { get; set; }
        public DateTime DateOfOrder { get; set; } = DateTime.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.New;
        public decimal FinalPrice { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
