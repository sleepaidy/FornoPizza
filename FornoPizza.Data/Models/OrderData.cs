using FornoPizza.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace FornoPizza.Data.Models
{
    public class OrderData
    {
        public int Id { get; set; } 
        public DateTime DateOfOrder { get; set; } = DateTime.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.New;
        public decimal FinalPrice { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        [MaxLength(300)]
        public string? Comment { get; set; }
        public int ClientId { get; set; }
        public int? UserId { get; set; }

        public virtual UserData? User { get; set; }
        public virtual ClientData? Client { get; set; }
        public virtual List<OrderItemData> OrderItems { get; set; } = new();
    }
}
