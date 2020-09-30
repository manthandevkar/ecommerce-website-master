using System.Collections.Generic;

namespace CP.API.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        public string SKU { get; set; }
        public string ProductNameEnglish { get; set; }
        public string ProductNameArabic { get; set; }
        public int ProductNumber { get; set; }
        public string ProductDescriptionEnglish { get; set; }
        public string ProductDescriptionArabic { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal VAT { get; set; }
        public decimal AmountOfTheAddedTax { get; set; }
        public decimal PriceAfterTax { get; set; }
        public int Quantity { get; set; }
        public decimal  ActualCost { get; set; }

        public SubCategory SubCategory { get; set; }
        public int SubCategoryId { get; set; }
        public Coupon Coupon { get; set; }
        public int CouponId { get; set; }

        public Vendor Vendor { get; set; }
        public int VendorId { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<PhotoForProduct> PhotoForProducts { get; set; }
        public ICollection<PaymentType> Payments {get; set;}
    }
}