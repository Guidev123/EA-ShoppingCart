using FluentValidation;

namespace ShoppingCart.API.Models.Validations
{
    public class OrderItemValidator : AbstractValidator<ItemCart>
    {
        public OrderItemValidator()
        {
            RuleFor(c => c.ProductId).NotEqual(Guid.Empty).WithMessage("Product id is invalid");
            RuleFor(c => c.Name).NotEmpty().WithMessage("Product name is invalid");
            RuleFor(c => c.Price).GreaterThan(0).WithMessage("Product price should be greater than $0");
            RuleFor(c => c.Quantity).GreaterThan(0).WithMessage("Product quantity should be greater than 0");
            RuleFor(c => c.Quantity).LessThan(CustomerCart.MAX_QUANTITY_ITEM)
                .WithMessage($"Product quantity should be less than {CustomerCart.MAX_QUANTITY_ITEM}");
        }
    }
}
