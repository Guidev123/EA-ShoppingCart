using Microsoft.EntityFrameworkCore;
using ShoppingCart.API.Data.Repositoreis.Interfaces;
using ShoppingCart.API.Models;

namespace ShoppingCart.API.Data.Repositoreis
{
    public class ItemCartRepository(CartDbContext context) : IItemCartRepository
    {
        private readonly CartDbContext _context = context;
        public async Task CreateAsync(ItemCart item)
        {
            await _context.ItemCart.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<ItemCart?> GetItemCartByIdAsync(Guid cartId, Guid productId) => 
            await _context.ItemCart.FirstOrDefaultAsync(x => x.CartId == cartId && x.ProductId == productId);

        public async Task RemoveAsync(ItemCart item)
        {
            _context.ItemCart.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ItemCart item)
        {
            _context.ItemCart.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
