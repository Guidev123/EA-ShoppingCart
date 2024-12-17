using ShoppingCart.API.Data.Repositoreis.Interfaces;
using ShoppingCart.API.Models;
using ShoppingCart.API.Responses;
using ShoppingCart.API.UseCases.Interfaces;

namespace ShoppingCart.API.Endpoints.ShoppingCart
{
    public class ApplyVoucherInCartEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapPost("/apply-voucher", HandleAsync).Produces<Response<CustomerCart?>>();

        private static async Task<IResult> HandleAsync(Voucher voucher, ICustomerCartRepository cartRepository, IUserUseCase user)
        {
            var customerCart = await cartRepository.GetByCustomerIdAsync(user.GetUserId());
            if(!customerCart.IsSuccess 
             || customerCart.Data is null) return TypedResults.BadRequest();

            customerCart.Data.ApplyVoucher(voucher);

            await cartRepository.UpdateAsync(customerCart.Data);

            return TypedResults.Ok();
        }

    }
}
