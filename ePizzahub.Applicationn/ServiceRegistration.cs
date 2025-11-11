using ePizzahub.Application.Contracts;
using ePizzahub.Applicationn.Contracts;
using ePizzahub.Applicationn.Implementation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzahub.Applicationn
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IUserTokenService, UserTokenService>();
            return services;
        }
    }
}
