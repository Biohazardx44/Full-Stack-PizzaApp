using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PizzaApp.DataAccess.Data;
using PizzaApp.DataAccess.Repositories.Abstraction;
using PizzaApp.DataAccess.Repositories.Implementation.EntityFrameworkImplementation;

namespace PizzaApp.Helpers.DependencyInjectionContainer
{
    public static class DependencyInjectionHelper
    {
        public static void InjectServices(this IServiceCollection services)
        {
            //services.AddTransient<,>();
            //services.AddTransient<,>();
        }

        public static void InjectRepositories(this IServiceCollection services)
        {
            // Entity Framework Implementation
            services.AddTransient<IPizzaRepository, PizzaRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
        }

        public static void InjectDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<PizzaAppDbContext>(options =>
                options.UseNpgsql(connectionString));
        }
    }
}