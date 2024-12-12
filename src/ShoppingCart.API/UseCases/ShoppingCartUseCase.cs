using EA.CommonLib.Responses;
using ShoppingCart.API.Data.Repositoreis.Interfaces;
using ShoppingCart.API.Models;
using ShoppingCart.API.UseCases.Interfaces;

namespace ShoppingCart.API.UseCases
{
    public class ShoppingCartUseCase(ICustomerCartRepository cartRepository,
                                     IItemCartRepository itemCartRepository)
                                   : IShoppingCartUseCase
    {
        private readonly ICustomerCartRepository _customerCartRepository = cartRepository;
        private readonly IItemCartRepository _itemCartRepository = itemCartRepository;

        public async Task<Response<CustomerCart>> HandleExistentShoppingCart(CustomerCart customerCart, ItemCart item)
        {
            var existentProductItem = customerCart.ItemCartAlreadyExists(item);

            var validationResult = item.Validate();
            if (!validationResult.IsValid)
                return new(null, 400, "Error", validationResult.Errors.Select(x => x.ErrorMessage).ToArray());

            customerCart.AddItem(item);

            if (existentProductItem) _itemCartRepository.Update(customerCart.GetProductById(item.ProductId));
            else await _itemCartRepository.CreateAsync(item);

            _customerCartRepository.Update(customerCart, item.ProductId);

            return new(null, 200);
        }

        public async Task<Response<CustomerCart>> HandleNewShoppingCart(CustomerCart customerCart, ItemCart item)
        {
            var validationResult = item.Validate();
            if (!validationResult.IsValid)
                return new(null, 400, "Error", validationResult.Errors.Select(x => x.ErrorMessage).ToArray());

            customerCart.AddItem(item);
            await _customerCartRepository.CreateAsync(customerCart);
            return new(null, 200);
        }
    }
}
