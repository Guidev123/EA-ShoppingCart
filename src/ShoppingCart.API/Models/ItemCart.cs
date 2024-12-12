using ShoppingCart.API.Models.Validations;

namespace ShoppingCart.API.Models
{
    public class ItemCart
    {
        public ItemCart(Guid productId, string name, decimal price, string image, Guid cartId)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Name = name;
            Price = price;
            Image = image;
            CartId = cartId;
        }
        protected ItemCart() { }    

        public Guid Id { get; }
        public Guid ProductId { get; }
        public string Name { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public string Image { get; private set; } = string.Empty;
        public Guid CartId { get; private set; }
        public CustomerCart CustomerCart { get; private set; } = null!;
        internal void AssociateCart(Guid cartId) => CartId = cartId;
        internal bool IsValid() => new OrderItemValidator().Validate(this).IsValid;
    }
}
