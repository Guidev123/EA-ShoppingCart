using ShoppingCart.API.Models;
using ShoppingCart.API.Responses;

namespace ShoppingCart.API.Data.Repositoreis.Interfaces
{
    public interface ICustomerCartRepository
    {
        Task<Response<CustomerCart>> GetByCustomerIdAsync(string id);
        Task CreateAsync(CustomerCart customerCart);
        Task UpdateAsync(CustomerCart customerCart);
    }
}
