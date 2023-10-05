using PizzaApp.Services.UserServices.Abstraction;
using System.Collections.Concurrent;

namespace PizzaApp.Services.UserServices.Implementation
{
    public class TokenProvider<T> : ITokenProvider<T>
    {
        private readonly ConcurrentDictionary<string, T?> Tokens = new();

        public Task<T?> GetTokenAsync(string key)
        {
            if (Tokens.TryGetValue(key, out var token))
                return Task.FromResult(token);

            return Task.FromResult(default(T));
        }

        public Task SetTokenAsync(string key, T value)
        {
            Tokens.TryAdd(key, value);
            return Task.CompletedTask;
        }
    }
}