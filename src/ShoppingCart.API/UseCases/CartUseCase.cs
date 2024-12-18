using FluentValidation;
using FluentValidation.Results;
using SharedLib.Domain.Responses;
using ShoppingCart.API.Data.Repositoreis.Interfaces;
using ShoppingCart.API.Models;
using ShoppingCart.API.Models.Validations;
using ShoppingCart.API.UseCases.Interfaces;

namespace ShoppingCart.API.UseCases
{
    public class CartUseCase(ICustomerCartRepository cartRepository,
                                     IItemCartRepository itemCartRepository)
                                   : ICartUseCase
    {
        private readonly ICustomerCartRepository _customerCartRepository = cartRepository;
        private readonly IItemCartRepository _itemCartRepository = itemCartRepository;


        public async Task<Response<CustomerCart>> HandleExistentShoppingCart(CustomerCart customerCart, ItemCart item)
        {
            var existentProductItem = customerCart.ItemCartAlreadyExists(item);

            var validationResult = ValidateEntity(new ItemCartValidator(), item);

            if (!validationResult.IsValid)
                return new(null, 400, "Error", GetAllErrors(validationResult));

            customerCart.AddItem(item);

            if (existentProductItem) await _itemCartRepository.UpdateAsync(customerCart.GetProductById(item.ProductId));
            else await _itemCartRepository.CreateAsync(item);

            await _customerCartRepository.UpdateAsync(customerCart);

            return new(null, 200);
        }

        public async Task<Response<CustomerCart>> HandleNewShoppingCart(CustomerCart customerCart, ItemCart item)
        {
            var validationResult = ValidateEntity(new ItemCartValidator(), item);

            if (!validationResult.IsValid)
                return new(null, 400, "Error", GetAllErrors(validationResult));

            customerCart.AddItem(item);
            await _customerCartRepository.CreateAsync(customerCart);
            return new(null, 200);
        }

        public async Task<Response<ItemCart>> UpdateItemCartAsync(ItemCart item, Guid productId, string customerId)
        {
            var customerCart = await _customerCartRepository.GetByCustomerIdAsync(customerId);
            if (customerCart.Data is null) return new(null, 404);

            var itemCart = await GetItemCartValidated(item, productId, customerCart.Data);
            if (itemCart.Data is null || !itemCart.IsSuccess)
                return new(null, 404, itemCart!.Message, itemCart.Errors);

            customerCart.Data.UpdateUnities(itemCart.Data, item.Quantity);

            await _itemCartRepository.UpdateAsync(itemCart.Data);
            await _customerCartRepository.UpdateAsync(customerCart.Data);

            return new(null, 200, customerCart.Message);
        }

        public async Task<Response<ItemCart>> RemoveItemCartAsync(Guid productId, string customerId)
        {
            var customerCart = await _customerCartRepository.GetByCustomerIdAsync(customerId);

            if (customerCart.Data is null) return new(null, 404);

            var itemCart = customerCart.Data.Itens.FirstOrDefault(x => x.Id == productId);
            if (itemCart is null) return new(null, 404);

            customerCart.Data.RemoveItem(itemCart);

            await _itemCartRepository.RemoveAsync(itemCart);
            await _customerCartRepository.UpdateAsync(customerCart.Data);

            return new(null, 200);
        }

        private async Task<Response<ItemCart>> GetItemCartValidated(ItemCart item, Guid productId, CustomerCart customerCart)
        {
            var validationResult = ValidateEntity(new ItemCartValidator(), item);

            if (!validationResult.IsValid)
                return new(item, 400, "Error", GetAllErrors(validationResult));

            if (productId != item.ProductId)
            {
                AddError(validationResult, "Item does not correspond to what is stated");
                return new(item, 400, "Error", GetAllErrors(validationResult)); 
            }

            if (customerCart is null)
            {
                AddError(validationResult, "Customer cart can not be empty");
                return new(item, 400, "Error", GetAllErrors(validationResult));
            }

            var cart = await _itemCartRepository.GetItemCartByIdAsync(item.CartId, productId);
            if(cart is null)
            {
                AddError(validationResult, "Item not found");
                return new(item, 400, "Error", GetAllErrors(validationResult));
            }

            return new(item, 200, "Success", GetAllErrors(validationResult));
        }

        private static ValidationResult ValidateEntity<TV, TE>(TV validation, TE entity) where TV
                     : AbstractValidator<TE> where TE : class => validation.Validate(entity);
        private static void AddError(ValidationResult validationResult, string message) =>
           validationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        private static string[] GetAllErrors(ValidationResult validationResult) =>
            validationResult.Errors.Select(e => e.ErrorMessage).ToArray();

    }
}
