using ePizzahub.Domain.Interfaces;
using ePizzahub.Infrastructure.Repositories;
using ePizzahub.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ePizzahub.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            // Register your infrastructure services here, e.g., DbContext, repositories, etc.

            services.AddDbContext<EPizzadbContext>(option =>
            {
                option.UseSqlServer(connectionString);
            });


            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICartItemRepository, CartItemRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IUserTokenRepository, UserTokenRepository>();

            return services;
        }
    }
}
