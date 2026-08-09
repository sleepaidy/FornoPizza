using System.ComponentModel.DataAnnotations;

namespace FornoPizza.Data.Models
{
    public class PizzaData
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(250)]
        public string Ingredients { get; set; } = string.Empty;
        [MaxLength(250)]
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } = true;


    }
}
