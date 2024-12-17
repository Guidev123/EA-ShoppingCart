using ShoppingCart.API.Endpoints.ShoppingCart;

namespace ShoppingCart.API.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var endpoints = app
                .MapGroup("");

            endpoints.MapGroup("api/v1/shopping-cart")
                .WithTags("ShoppingCart")
                .MapEndpoint<ApplyVoucherInCartEndpoint>()
                .MapEndpoint<CreateCartEndpoint>()
                .MapEndpoint<GetCartEndpoint>()
                .MapEndpoint<RemoveItemCartEndpoint>()
                .MapEndpoint<UpdateItemCartEndpoint>();
        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
            where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}
