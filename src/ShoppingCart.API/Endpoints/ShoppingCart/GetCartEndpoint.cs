using SharedLib.Domain.Responses;
using ShoppingCart.API.Data.Repositoreis.Interfaces;
using ShoppingCart.API.Models;
using ShoppingCart.API.UseCases.Interfaces;
using System.Security.Claims;

namespace ShoppingCart.API.Endpoints.ShoppingCart
{
    public class GetCartEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapGet("/", HandleAsync).Produces<Response<CustomerCart?>>();

        private static async Task<IResult> HandleAsync(IUserUseCase user,
                                                       ClaimsPrincipal userClaims,
                                                       ICustomerCartRepository cartRepository)
        {
            var customerCart = await cartRepository.GetByCustomerIdAsync(user.GetUserId(userClaims));

            return customerCart.IsSuccess
                ? TypedResults.Ok(customerCart)
                : TypedResults.NotFound(customerCart);
        }
    }
}
