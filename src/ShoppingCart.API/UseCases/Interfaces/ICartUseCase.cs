using ShoppingCart.API.Models;
using ShoppingCart.API.Responses;

namespace ShoppingCart.API.UseCases.Interfaces
{
    public interface ICartUseCase
    {
        Task<Response<CustomerCart>> HandleNewShoppingCart(CustomerCart customerCart, ItemCart item);
        Task<Response<CustomerCart>> HandleExistentShoppingCart(CustomerCart customerCart, ItemCart item);
        Task<Response<ItemCart>> UpdateItemCartAsync(ItemCart item, Guid productId, string customerId);
        Task<Response<ItemCart>> RemoveItemCartAsync(Guid productId, string customerId);
    }
}
