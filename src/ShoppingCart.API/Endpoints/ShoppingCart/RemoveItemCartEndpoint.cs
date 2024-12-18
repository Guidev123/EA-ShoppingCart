using SharedLib.Domain.Responses;
using ShoppingCart.API.Data.Repositoreis.Interfaces;
using ShoppingCart.API.Models;
using ShoppingCart.API.UseCases.Interfaces;
using System.Security.Claims;

namespace ShoppingCart.API.Endpoints.ShoppingCart
{
    public class RemoveItemCartEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapDelete("/{productId:guid}", HandleAsync).Produces<Response<CustomerCart?>>();

        private static async Task<IResult> HandleAsync(Guid productId,
                                                       IUserUseCase user,
                                                       ClaimsPrincipal userClaims,
                                                       ICustomerCartRepository cartRepository,
                                                       ICartUseCase cart)
        {
            var result = await cart.RemoveItemCartAsync(productId, user.GetUserId(userClaims));
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
