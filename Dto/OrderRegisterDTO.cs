using System;
using System.ComponentModel.DataAnnotations;
namespace CP.API.Dto
{
    public class OrderRegisterDTO
    {
        public int OrderId { get; set; }
        [RegularExpression("/^[^ا-ي A-z]+$/", ErrorMessage = "Category Name must Not Contains Number !!")]

        public int OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public int CustomerId { get; set; }

        public int PaymentId { get; set; }
        public int ShipperId { get; set; }

        public OrderRegisterDTO()
        {
        
            OrderDate = DateTime.Now;
            
        }
    }
}