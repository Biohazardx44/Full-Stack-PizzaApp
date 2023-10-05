namespace PizzaApp.Services.UserServices.Abstraction
{
    public interface ITokenProvider<T>
    {
        Task<T?> GetTokenAsync(string key);
        Task SetTokenAsync(string key, T value);
    }
}