using ShoppingCart.API.Models;

namespace ShoppingCart.API.Data.Repositoreis.Interfaces
{
    public interface IItemCartRepository
    {
        Task<ItemCart?> GetItemCartByIdAsync(Guid cartId, Guid productId);
        Task CreateAsync(ItemCart item);
        Task UpdateAsync(ItemCart item);
        Task RemoveAsync(ItemCart item);
    }
}
