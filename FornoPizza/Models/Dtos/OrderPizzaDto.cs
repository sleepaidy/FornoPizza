namespace FornoPizza.Models.Dtos
{
    public class OrderPizzaDto
    {
        public int PizzaId { get; set; }
        public string PizzaName { get; set; } = string.Empty;
        public decimal PizzaPrice { get; set; }
    }
}
