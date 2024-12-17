using ShoppingCart.API.Enums;

namespace ShoppingCart.API.Models
{
    public class Voucher
    {
        public Voucher(decimal? percentual, decimal? discountValue, string code, EDiscountType discountType)
        {
            Percentual = percentual;
            DiscountValue = discountValue;
            Code = code;
            DiscountType = discountType;
        }

        public decimal? Percentual { get; private set; }
        public decimal? DiscountValue { get; private set; }
        public string Code { get; private set; }
        public EDiscountType DiscountType { get; private set; }
    }
}
