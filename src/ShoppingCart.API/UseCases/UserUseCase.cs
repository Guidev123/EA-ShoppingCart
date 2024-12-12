using ShoppingCart.API.UseCases.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ShoppingCart.API.UseCases
{
    public class UserUseCase(IHttpContextAccessor httpContextAccessor) : IUserUseCase
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        public string GetUserId() =>
             _httpContextAccessor.HttpContext?.User.Claims.
            FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value
            ?? string.Empty;
    }

}
