using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PizzaApp.DataAccess.Data;
using PizzaApp.DataAccess.Repositories.Abstraction;
using PizzaApp.DataAccess.Repositories.Implementation.EntityFrameworkImplementation;
using PizzaApp.Services.Abstraction;
using PizzaApp.Services.Implementation;
using PizzaApp.Services.UserServices.Abstraction;
using PizzaApp.Services.UserServices.Implementation;

namespace PizzaApp.Helpers.DependencyInjectionContainer
{
    public static class DependencyInjectionHelper
    {
        public static void InjectServices(this IServiceCollection services)
        {
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPizzaService, PizzaService>();
            services.AddTransient<IOrderService, OrderService>();
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