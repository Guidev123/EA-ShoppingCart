using SharedLib.Domain.Responses;
using ShoppingCart.API.Data.Repositoreis.Interfaces;
using ShoppingCart.API.Models;
using ShoppingCart.API.UseCases.Interfaces;
using System.Security.Claims;

namespace ShoppingCart.API.Endpoints.ShoppingCart
{
    public class CreateCartEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapPost("/", HandleAsync).Produces<Response<CustomerCart?>>();

        private static async Task<IResult> HandleAsync(ItemCart item,
                                                       IUserUseCase user,
                                                       ClaimsPrincipal userClaims,
                                                       ICustomerCartRepository cartRepository,
                                                       ICartUseCase cartUseCase)
        {
            var customerId = user.GetUserId(userClaims);

            var customerCart = await cartRepository.GetByCustomerIdAsync(customerId);

            var result = !customerCart.IsSuccess
                ? await cartUseCase.HandleNewShoppingCart(new(customerId), item)
                : await cartUseCase.HandleExistentShoppingCart(new(customerId), item);

            return result.IsSuccess
                ? TypedResults.Created(string.Empty, result) 
                : TypedResults.BadRequest(result);
        }
    }
}
