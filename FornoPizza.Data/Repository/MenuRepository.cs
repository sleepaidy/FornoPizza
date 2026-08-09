using FornoPizza.Data.Models;
using FornoPizza.Data.Repository.Interfaces;

namespace FornoPizza.Data.Repository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly WebContext _webContext;

        public MenuRepository(WebContext webContext)
        {
            _webContext = webContext;
        }

        public List<PizzaData> GetPizzaMenu()
        {
            return _webContext.Pizzas.Where(x => x.IsAvailable).ToList();
        }

        public List<ToppingData> GetToppingsMenu()
        {
            return _webContext.Toppings.Where(x => x.IsAvailable).ToList();
        }
    }
}
