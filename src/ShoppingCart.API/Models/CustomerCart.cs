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
        public bool VoucherIsUsed { get; private set; }
        public decimal Discount { get; private set; }
        public Voucher Voucher { get; private set; } = null!;

        internal void CalculateTotalPrice()
        {
            TotalPrice = Itens.Sum(x => x.CalculateValue());
            CalculateTotalPriceDiscount();
        }
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
            CalculateTotalPrice();
        }

        internal void UpdateItem(ItemCart item)
        {
            item.AssociateCart(Id);

            var existentItem = GetProductById(item.ProductId);

            Itens.Remove(existentItem);
            Itens.Add(item);

            CalculateTotalPrice();
        }

        internal void RemoveItem(ItemCart item)
        {
            Itens.Remove(GetProductById(item.ProductId));
            CalculateTotalPrice();
        }

        internal void UpdateUnities(ItemCart item, int unities)
        {
            item.UpdateUnities(unities);
            UpdateItem(item);
        }

        public void ApplyVoucher(Voucher voucher)
        {
            Voucher = voucher;
            VoucherIsUsed = true;
            CalculateTotalPrice();
        }

        private void CalculateTotalPriceDiscount()
        {
            if(!VoucherIsUsed) return;

            decimal discount = 0;
            var value = TotalPrice;

            if(Voucher.DiscountType == Enums.EDiscountType.Percentual)
            {
                if (Voucher.Percentual.HasValue)
                {
                    discount = (value * Voucher.Percentual.Value) / 100;
                    value -= discount;
                }
            }
            else
            {
                if (Voucher.DiscountValue.HasValue)
                {
                    discount = Voucher.DiscountValue.Value;
                    value -= discount;
                }
            }

            TotalPrice = value < 0 ? 0 : value;
            Discount = discount;
        }
    }
}
