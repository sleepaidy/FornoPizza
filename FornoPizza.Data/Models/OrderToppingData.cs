using System.ComponentModel.DataAnnotations;

namespace FornoPizza.Data.Models
{
    public class OrderToppingData
    {
        public int Id { get; set; }
        public int ToppingId { get; set; }
        public int OrderItemId { get; set; }
        [MaxLength(100)]
        public string ToppingName { get; set; } = string.Empty;
        public decimal ToppingPrice { get; set; }

        public virtual OrderItemData? OrderItem { get; set; }
        public virtual ToppingData? Topping { get; set; }
    }
}
