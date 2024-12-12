using EA.CommonLib.Responses;
using ShoppingCart.API.Models;

namespace ShoppingCart.API.Data.Repositoreis.Interfaces
{
    public interface IShoppingCartRepository
    {
        Task<Response<CustomerCart>> GetByIdAsync(string id);
        Task CreateAsync(CustomerCart customerCart);
    }
}
