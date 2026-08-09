using FornoPizza.Data.Models;

namespace FornoPizza.Data.Repository.Interfaces
{
    public interface IMenuRepository
    {
        public List<PizzaData> GetPizzaMenu();
        public List<ToppingData> GetToppingsMenu();
    }
}
