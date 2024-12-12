using Microsoft.EntityFrameworkCore;
using ShoppingCart.API.Data;
using ShoppingCart.API.Data.Repositoreis;
using ShoppingCart.API.Data.Repositoreis.Interfaces;
using ShoppingCart.API.UseCases;
using ShoppingCart.API.UseCases.Interfaces;

namespace ShoppingCart.API.Middlewares
{
    public static class ApplicationModule
    {
        public static void AddDbContext(this WebApplicationBuilder builder)=>
            builder.Services.AddDbContext<CartDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty));
        public static void AddRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ICustomerCartRepository, CustomerCartRepository>();
            builder.Services.AddTransient<IItemCartRepository, ItemCartRepository>();
        }

        public static void AddUseCases(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IUserUseCase, UserUseCase>();
            builder.Services.AddTransient<ICartUseCase, CartUseCase>();
        }
    }
}
