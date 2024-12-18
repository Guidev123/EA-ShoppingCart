using SharedLib.Domain.Responses;
using ShoppingCart.API.Models;

namespace ShoppingCart.API.Data.Repositoreis.Interfaces
{
    public interface ICustomerCartRepository
    {
        Task<Response<CustomerCart>> GetByCustomerIdAsync(string id);
        Task CreateAsync(CustomerCart customerCart);
        Task UpdateAsync(CustomerCart customerCart);
        Task DeleteAsync(CustomerCart customerCart);
    }
}
