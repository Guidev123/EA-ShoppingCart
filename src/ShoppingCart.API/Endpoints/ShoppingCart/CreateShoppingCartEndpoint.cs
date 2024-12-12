
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
                                                       ClaimsPrincipal user,
                                                       IShoppingCartRepository cartRepository,
                                                       IShoppingCartUseCase cartUseCase)
        {
            var customerIdClaim = user.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            if (customerIdClaim == null)
                return TypedResults.BadRequest("User ID not found in token.");

            var customerCart = await cartRepository.GetByIdAsync(customerIdClaim);

            if (!customerCart.IsSuccess)
            {
                await cartUseCase.HandleNewShoppingCart(new(customerIdClaim), item);
            }

            return TypedResults.Ok(customerCart);
        }
    }
}
