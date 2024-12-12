using FluentValidation;

namespace ShoppingCart.API.Models.Validations
{
    public class CustomerCartValidation : AbstractValidator<CustomerCart>
    {
        public CustomerCartValidation()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer not found");
            RuleFor(x => x.Itens.Count).GreaterThan(0).WithMessage("Cart has no itens");
            RuleFor(x => x.TotalPrice).GreaterThan(0).WithMessage("Cart price should be greater than $0");
        }
    }
}
