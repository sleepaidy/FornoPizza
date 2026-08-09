using FornoPizza.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace FornoPizza.Data.Models
{
    public class OrderItemData
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int PizzaId { get; set; }
        [MaxLength(100)]
        public string PizzaNameInOrder { get; set; } = string.Empty;
        public decimal PizzaPriceInOrder { get; set; }
        public Size Size { get; set; }
        public Dough Dough { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal LinePrice { get; set; }

        public virtual List<OrderToppingData> OrderToppings { get; set; } = new();
        public virtual PizzaData? Pizza { get; set; } 
        public virtual OrderData? Order { get; set; } 
    }
}
