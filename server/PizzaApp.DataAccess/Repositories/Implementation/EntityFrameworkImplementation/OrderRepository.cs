using PizzaApp.DataAccess.Data;
using PizzaApp.DataAccess.Repositories.Abstraction;
using PizzaApp.Domain.Entities;

namespace PizzaApp.DataAccess.Repositories.Implementation.EntityFrameworkImplementation
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly PizzaAppDbContext _pizzaAppDbContext;

        public OrderRepository(PizzaAppDbContext pizzaAppDbContext) : base(pizzaAppDbContext)
        {
            _pizzaAppDbContext = pizzaAppDbContext;
        }
    }
}