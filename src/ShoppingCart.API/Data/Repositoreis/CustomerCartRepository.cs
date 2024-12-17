using Microsoft.EntityFrameworkCore;
using ShoppingCart.API.Data.Repositoreis.Interfaces;
using ShoppingCart.API.Models;
using ShoppingCart.API.Responses;

namespace ShoppingCart.API.Data.Repositoreis
{
    public class CustomerCartRepository(CartDbContext dbContext) : ICustomerCartRepository
    {
        private readonly CartDbContext _dbContext = dbContext;

        public async Task CreateAsync(CustomerCart customerCart)
        {
            await _dbContext.CustomerCart.AddAsync(customerCart);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(CustomerCart cart)
        {
            _dbContext.CustomerCart.Remove(cart);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Response<CustomerCart>> GetByCustomerIdAsync(string id)
        {
            var cart = await _dbContext.CustomerCart.Include(x => x.Itens).FirstOrDefaultAsync(x => x.CustomerId == id);
            return cart is not null ? new Response<CustomerCart>(cart, 200) : new Response<CustomerCart>(null, 404);
        }

        public async Task UpdateAsync(CustomerCart customerCart)
        {
            _dbContext.CustomerCart.Update(customerCart);
            await _dbContext.SaveChangesAsync();
        }
    }
}
