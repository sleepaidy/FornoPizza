using System.ComponentModel.DataAnnotations;

namespace FornoPizza.Data.Models
{
    public class ToppingData
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public decimal Price { get; set; }

        public virtual List<OrderToppingData> OrderToppings { get; set; } = new();
    }
}
