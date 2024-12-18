﻿using SharedLib.Domain.Responses;
using ShoppingCart.API.Models;

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
