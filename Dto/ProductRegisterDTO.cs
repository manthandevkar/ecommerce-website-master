using System.Collections.Generic;
using CP.API.Model;
using System.ComponentModel.DataAnnotations;

namespace CP.API.Dto
{
    public class ProductRegisterDTO
    {
        [RegularExpression("/^[^ا-ي A-z]+$/", ErrorMessage = "Category Name must Not Contains Number !!")]

        public int ProductNumber { get; set; }
        public string SKU { get; set; }
        [RegularExpression(" /^[^0-9]+$/", ErrorMessage = "Product Name English  must Not Contains Number !!")]

        public string ProductNameEnglish { get; set; }
        [RegularExpression(" /^[^0-9]+$/", ErrorMessage = "Product Name Arabic  must Not Contains Number !!")]

        public string ProductNameArabic { get; set; }
        [RegularExpression(" /^[^0-9]+$/", ErrorMessage = "Product Description English  must Not Contains Number !!")]

        public string ProductDescriptionEnglish { get; set; }
        [RegularExpression(" /^[^0-9]+$/", ErrorMessage = "Product Description Arabic  must Not Contains Number !!")]

        public string ProductDescriptionArabic { get; set; }
        [RegularExpression("/^[^ا-ي A-z]+$/", ErrorMessage = "Unit Price must Not Contains Number !!")]

        public decimal UnitPrice { get; set; }
        public decimal VAT { get; set; }
        [RegularExpression("/^[^ا-ي A-z]+$/", ErrorMessage = "Unit Price must Not Contains Number !!")]

        public decimal AmountOfTheAddedTax { get; set; }
        [RegularExpression("/^[^ا-ي A-z]+$/", ErrorMessage = "Unit Price must Not Contains Number !!")]

        public decimal PriceAfterTax { get; set; }
        [RegularExpression("/^[^ا-ي A-z]+$/", ErrorMessage = "Quantity must Not Contains Number !!")]

        public int Quantity { get; set; }
        [RegularExpression("/^[^ا-ي A-z]+$/", ErrorMessage = "Actual Cost must Not Contains Number !!")]

        public decimal ActualCost { get; set; }
        public int SubCategoryId { get; set; }
        public int VendorId { get; set; }

        public ProductRegisterDTO(decimal vAT , decimal unitPrice  )
        {
            VAT = vAT;
            UnitPrice = unitPrice;
            AmountOfTheAddedTax =  (VAT * UnitPrice);
            PriceAfterTax = unitPrice + AmountOfTheAddedTax;
        }

    }
}