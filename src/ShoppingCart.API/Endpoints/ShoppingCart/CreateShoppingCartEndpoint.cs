using EA.CommonLib.Responses;
using ShoppingCart.API.Data.Repositoreis.Interfaces;
using ShoppingCart.API.Models;
using ShoppingCart.API.UseCases.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ShoppingCart.API.Endpoints.ShoppingCart
{
    public class CreateShoppingCartEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
                .WithOrder(1)
                .Produces<Response<CustomerCart?>>();

        private static async Task<IResult> HandleAsync(ItemCart item,
                                                       IUserUseCase user,
                                                       ICustomerCartRepository cartRepository,
                                                       IShoppingCartUseCase cartUseCase)
        {
            var userId = user.GetUserId();

            var customerCart = await cartRepository.GetByIdAsync(userId);

            var result = !customerCart.IsSuccess
                ? await cartUseCase.HandleNewShoppingCart(new(userId), item)
                : await cartUseCase.HandleExistentShoppingCart(new(userId), item);

            return result.IsSuccess
                ? TypedResults.Created(string.Empty, result) 
                : TypedResults.BadRequest(result);
        }
    }
}
