using PizzaApp.DataAccess.Data;
using PizzaApp.DataAccess.Repositories.Abstraction;
using PizzaApp.Domain.Entities;

namespace PizzaApp.DataAccess.Repositories.Implementation.EntityFrameworkImplementation
{
    public class PizzaRepository : BaseRepository<Pizza>, IPizzaRepository
    {
        private readonly PizzaAppDbContext _pizzaAppDbContext;

        public PizzaRepository(PizzaAppDbContext pizzaAppDbContext) : base(pizzaAppDbContext)
        {
            _pizzaAppDbContext = pizzaAppDbContext;
        }
    }
}