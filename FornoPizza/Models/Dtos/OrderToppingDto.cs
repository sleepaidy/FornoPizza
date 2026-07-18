namespace FornoPizza.Models.Dtos
{
    public class OrderToppingDto
    {
        public int ToppingId { get; set; }
        public string ToppingName { get; set; } = string.Empty;
        public decimal ToppingPrice { get; set; }
    }
}
