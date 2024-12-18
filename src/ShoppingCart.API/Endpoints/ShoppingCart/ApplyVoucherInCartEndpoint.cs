using SharedLib.Domain.Responses;
using ShoppingCart.API.Data.Repositoreis.Interfaces;
using ShoppingCart.API.Models;
using ShoppingCart.API.UseCases.Interfaces;
using System.Security.Claims;

namespace ShoppingCart.API.Endpoints.ShoppingCart
{
    public class ApplyVoucherInCartEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapPost("/apply-voucher", HandleAsync).Produces<Response<CustomerCart?>>();

        private static async Task<IResult> HandleAsync(Voucher voucher,
                                                       ICustomerCartRepository cartRepository,
                                                       ClaimsPrincipal userClaims,
                                                       IUserUseCase user)
        {
            var customerCart = await cartRepository.GetByCustomerIdAsync(user.GetUserId(userClaims));
            if(!customerCart.IsSuccess 
             || customerCart.Data is null) return TypedResults.BadRequest();

            customerCart.Data.ApplyVoucher(voucher);

            await cartRepository.UpdateAsync(customerCart.Data);

            return TypedResults.Ok();
        }

    }
}
