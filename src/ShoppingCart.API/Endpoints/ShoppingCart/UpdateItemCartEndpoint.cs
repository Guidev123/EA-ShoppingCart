using SharedLib.Domain.Responses;
using ShoppingCart.API.Data.Repositoreis.Interfaces;
using ShoppingCart.API.Models;
using ShoppingCart.API.UseCases.Interfaces;
using System.Security.Claims;

namespace ShoppingCart.API.Endpoints.ShoppingCart
{
    public class UpdateItemCartEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapPut("/{productId:guid}", HandleAsync).Produces<Response<CustomerCart?>>();
        
        private static async Task<IResult> HandleAsync(Guid productId,
                                                       ItemCart item,
                                                       IUserUseCase user,
                                                       ClaimsPrincipal userClaims,
                                                       ICustomerCartRepository cartRepository,
                                                       ICartUseCase cartUseCase)
        {
            var customerId = user.GetUserId(userClaims);
            var customerCart = await cartRepository.GetByCustomerIdAsync(customerId);

            var result = await cartUseCase.UpdateItemCartAsync(item, productId, customerId);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);

        }
    }
}
