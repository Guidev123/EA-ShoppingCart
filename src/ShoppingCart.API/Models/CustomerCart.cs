using System.Linq;

namespace ShoppingCart.API.Models
{
    public class CustomerCart
    {
        internal const int MAX_QUANTITY_ITEM = 5;
        public CustomerCart(string customerId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
        }
        protected CustomerCart() { }

        public Guid Id { get; }
        public string CustomerId { get; } = string.Empty;
        public decimal TotalPrice { get; private set; }
        public List<ItemCart> Itens { get; private set; } = [];

        internal void GetTotalPrice() => TotalPrice = Itens.Sum(x => x.CalculateValue());
        internal ItemCart GetProductById(Guid productId) => Itens.First(x => x.ProductId == productId);
        internal bool ItemCartAlreadyExists(ItemCart item) => Itens.Any(x => x.ProductId == item.ProductId);
        internal void AddItem(ItemCart item)
        {
            item.AssociateCart(Id);

            if (ItemCartAlreadyExists(item))
            {
                var itemExists = GetProductById(item.ProductId);
                itemExists.AddUnity(item.Quantity);
                item = itemExists;
                Itens.Remove(itemExists);
            }

            Itens.Add(item);
            GetTotalPrice();
        }
    }
}
