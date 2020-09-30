using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CP.API.Model
{
    public class PaymentType
    {
        [Key]
        public int PaymentId { get; set; }
        public string PaymentTypes { get; set; }

        public ICollection<Order> Orders {get; set;}

         public Product Product { get; set; }
         public int ProductId { get; set; }
    }
}