using EA.CommonLib.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using ShoppingCart.API.Data.Repositoreis.Interfaces;
using ShoppingCart.API.Models;
using ShoppingCart.API.UseCases.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ShoppingCart.API.Endpoints.ShoppingCart
{
    public class GetShoppingCartEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapGet("/", HandleAsync)
                .WithOrder(1)
                .Produces<Response<CustomerCart?>>();

        private static async Task<IResult> HandleAsync(IUserUseCase user, ICustomerCartRepository cartRepository)
        {
            var customerIdClaim = user.GetUserId();

            var customerCart = await cartRepository.GetByIdAsync(customerIdClaim);

            return customerCart.IsSuccess
                ? TypedResults.Ok(customerCart)
                : TypedResults.NotFound(customerCart);
        }
    }
}
