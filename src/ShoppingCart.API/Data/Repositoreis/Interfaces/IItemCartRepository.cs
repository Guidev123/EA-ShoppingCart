using ShoppingCart.API.Models;

namespace ShoppingCart.API.Data.Repositoreis.Interfaces
{
    public interface IItemCartRepository
    {
        Task CreateAsync(ItemCart item);
        void Update(ItemCart item);
    }
}
