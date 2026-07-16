using System.ComponentModel.DataAnnotations;

namespace FornoPizza.Data.Models
{
    public class PizzaData
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Ingredients { get; set; }
        [MaxLength(250)]
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        
    }
}
