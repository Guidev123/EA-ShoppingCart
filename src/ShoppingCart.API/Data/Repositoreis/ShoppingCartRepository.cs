using EA.CommonLib.Responses;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.API.Data.Repositoreis.Interfaces;
using ShoppingCart.API.Models;

namespace ShoppingCart.API.Data.Repositoreis
{
    public class ShoppingCartRepository(CartDbContext dbContext) : IShoppingCartRepository
    {
        private readonly CartDbContext _dbContext = dbContext;

        public async Task CreateAsync(CustomerCart customerCart)
        {
            await _dbContext.AddAsync(customerCart);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Response<CustomerCart>> GetByIdAsync(string id)
        {
            var cart = await _dbContext.CustomerCart.Include(x => x.Itens).FirstOrDefaultAsync(x => x.CustomerId == id);
            return cart is not null ? new Response<CustomerCart>(cart, 200) : new Response<CustomerCart>(null, 404);
        }
    }
}
