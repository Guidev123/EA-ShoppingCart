using ShoppingCart.API.Models;

namespace ShoppingCart.API.UseCases.Interfaces
{
    public interface IShoppingCartUseCase
    {
        Task HandleNewShoppingCart(CustomerCart customerCart, ItemCart item);
    }
}
