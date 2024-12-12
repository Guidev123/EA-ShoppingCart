using EA.CommonLib.Responses;
using ShoppingCart.API.Models;

namespace ShoppingCart.API.UseCases.Interfaces
{
    public interface IShoppingCartUseCase
    {
        Task<Response<CustomerCart>> HandleNewShoppingCart(CustomerCart customerCart, ItemCart item);
        Task<Response<CustomerCart>> HandleExistentShoppingCart(CustomerCart customerCart, ItemCart item);
    }
}
