namespace FornoPizza.Models.Home
{
    public class PizzaMenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;  
        public string Ingredients { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
