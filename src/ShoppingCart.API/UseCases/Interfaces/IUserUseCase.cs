using System.Security.Claims;

namespace ShoppingCart.API.UseCases.Interfaces
{
    public interface IUserUseCase
    {
        string GetUserId(ClaimsPrincipal principal);
    }
}
