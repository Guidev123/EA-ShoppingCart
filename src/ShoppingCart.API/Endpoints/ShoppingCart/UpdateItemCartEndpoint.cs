using ShoppingCart.API.Models;
using ShoppingCart.API.Responses;

namespace ShoppingCart.API.Endpoints.ShoppingCart
{
    public class UpdateItemCartEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapPut("/{productId:guid}", HandleAsync).Produces<Response<CustomerCart?>>();
        
        private static async Task<IResult> HandleAsync(Guid productId, ItemCart item)
        {
            return TypedResults.Ok();
        }
    }
}
