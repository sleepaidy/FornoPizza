using FornoPizza.Models.Dtos;
using FornoPizza.Data.Enums;

namespace FornoPizza.Services.Interfaces
{
    public interface IPricingService
    {
        public FinalOrderDto Calculate(int pizzaId, Size size, Dough dough, IEnumerable<int> toppingIds, int quantity);
    }
}
