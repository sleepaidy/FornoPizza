using System.ComponentModel.DataAnnotations;

namespace FornoPizza.Data.Models
{
    public class ToppingData
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } = true;
        public virtual List<OrderToppingData> OrderToppings { get; set; } = new();
    }
}
