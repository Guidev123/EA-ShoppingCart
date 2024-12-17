using ShoppingCart.API.Data.Repositoreis.Interfaces;
using ShoppingCart.API.Models;
using ShoppingCart.API.Responses;
using ShoppingCart.API.UseCases.Interfaces;

namespace ShoppingCart.API.Endpoints.ShoppingCart
{
    public class GetCartEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapGet("/", HandleAsync).Produces<Response<CustomerCart?>>();

        private static async Task<IResult> HandleAsync(IUserUseCase user, ICustomerCartRepository cartRepository)
        {
            var customerCart = await cartRepository.GetByCustomerIdAsync(user.GetUserId());

            return customerCart.IsSuccess
                ? TypedResults.Ok(customerCart)
                : TypedResults.NotFound(customerCart);
        }
    }
}
