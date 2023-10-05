using Microsoft.EntityFrameworkCore;
using PizzaApp.DataAccess.Data;
using PizzaApp.DataAccess.Repositories.Abstraction;
using PizzaApp.Domain.Entities;

namespace PizzaApp.DataAccess.Repositories.Implementation.EntityFrameworkImplementation
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly PizzaAppDbContext _pizzaAppDbContext;

        public BaseRepository(PizzaAppDbContext pizzaAppDbContext)
        {
            _pizzaAppDbContext = pizzaAppDbContext;
        }

        public async Task AddAsync(T entity)
        {
            _pizzaAppDbContext.Set<T>().Add(entity);
            await SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _pizzaAppDbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return _pizzaAppDbContext.Set<T>().Find(id);
        }

        public async Task RemoveAsync(T entity)
        {
            _pizzaAppDbContext.Remove(entity);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _pizzaAppDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(T entity)
        {
            _pizzaAppDbContext.Set<T>().Update(entity);
            await SaveChangesAsync();
        }
    }
}