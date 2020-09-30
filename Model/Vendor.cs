using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CP.API.Model
{
    public class Vendor
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int IsActive { get; set; } 
        public DateTime DateOfAdded { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
