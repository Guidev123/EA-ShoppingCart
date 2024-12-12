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

        public void Update(ItemCart item) =>
            _context.ItemCart.Update(item);
    }
}
