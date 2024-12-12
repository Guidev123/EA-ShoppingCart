using ShoppingCart.API.Data.Repositoreis.Interfaces;
using ShoppingCart.API.Models;
using ShoppingCart.API.UseCases.Interfaces;

namespace ShoppingCart.API.UseCases
{
    public class ShoppingCartUseCase(IShoppingCartRepository cartRepository) : IShoppingCartUseCase
    {
        private readonly IShoppingCartRepository _cartRepository = cartRepository;
        public async Task HandleNewShoppingCart(CustomerCart customerCart, ItemCart item)
        {
            customerCart.AddItem(item);
            await _cartRepository.CreateAsync(customerCart);
        }
    }
}
