namespace FornoPizza.Models.Dtos
{
    public class FinalOrderDto
    {
        public decimal FinalPrice { get; set; }
        public int Quantity { get; set; } = 1;
        public OrderPizzaDto OrderPizza { get; set; }= new();
        public List<OrderToppingDto> OrderToppings { get; set; } = new();
    }
}
