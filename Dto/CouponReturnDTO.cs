using System;
using System.ComponentModel.DataAnnotations;
namespace CP.API.Dto
{
    public class CouponReturnDTO
    {
        public int CoupanId { get; set; }
        public string CoupanTitle { get; set; }

        public string CouponCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
        public int MinimumSpend { get; set; }
        public int MaximumSpend { get; set; }
    }
}