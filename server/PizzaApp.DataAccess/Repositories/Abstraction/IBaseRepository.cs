namespace PizzaApp.DataAccess.Repositories.Abstraction
{
    public interface IBaseRepository<T>
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task<T> GetByIdAsync(object id);
        Task<List<T>> GetAllAsync();
        Task SaveChangesAsync();
    }
}