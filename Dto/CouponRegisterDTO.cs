using System;
using System.ComponentModel.DataAnnotations;
using CP.API.Validations;
namespace CP.API.Dto
{
    public class CouponRegisterDTO
    {
        [Required(ErrorMessage = "Coupan Title is required")]
        public string CoupanTitle { get; set; }
        [Required(ErrorMessage = "CouponCode is required")]
        public string CouponCode { get; set; }
        [StartDateValidation(ErrorMessage ="start Date Must be less Than Current Time ")]
        public DateTime StartDate { get; set; }
        [EndDateValidation(ErrorMessage = "End Date Must be less Than Current Time and Grater Than Start Date ")]
        public DateTime EndDate { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
        [MinimumSpendValidation(ErrorMessage = "Minimum Spend Must Be Less Than Maximum Spend")]
        public int MinimumSpend { get; set; }
        [MaximumSpendValidation(ErrorMessage = "Maximum Spend Must Be Grater Than Minimum Spend")]
        public int MaximumSpend { get; set; }
    }
}