using EA.CommonLib.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using ShoppingCart.API.Data.Repositoreis.Interfaces;
using ShoppingCart.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ShoppingCart.API.Endpoints.ShoppingCart
{
    public class GetShoppingCartEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapGet("/", HandleAsync)
                .WithOrder(1)
                .Produces<Response<CustomerCart?>>();

        private static async Task<IResult> HandleAsync(ClaimsPrincipal user, IShoppingCartRepository cartRepository)
        {
            var customerIdClaim = user.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
            if (customerIdClaim == null)
                return TypedResults.BadRequest("User ID not found in token.");

            var customerCart = await cartRepository.GetByIdAsync(customerIdClaim!.Value);

            return customerCart.IsSuccess
                ? TypedResults.Ok(customerCart)
                : TypedResults.NotFound(customerCart);
        }
    }
}
