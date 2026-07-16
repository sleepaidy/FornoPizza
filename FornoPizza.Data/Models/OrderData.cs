using FornoPizza.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace FornoPizza.Data.Models
{
    public class OrderData
    {
        public int Id { get; set; } 
        public DateTime DateOfOrder { get; set; }
        public Status Status { get; set; }
        public decimal FinalPrice { get; set; }
        public Size Size { get; set; }
        public Dough Dough { get; set; }
        public int Amount { get; set; }
        public int PizzaId { get; set; }
        [MaxLength(100)]
        public string PizzaNameInOrder { get; set; }
        public decimal PizzaPriceInOrder { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        [MaxLength(300)]
        public string? Comment { get; set; }
        public int ClientId { get; set; }
        

        public virtual List<OrderToppingData> OrderToppings { get; set; } = new();
        public virtual ClientData? Client { get; set; }
        public virtual PizzaData? Pizza { get; set; }
    }
}
