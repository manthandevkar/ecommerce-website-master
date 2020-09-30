using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace CP.API.Model
{
    public class User: IdentityUser<int>
    {
        public string Phone { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public DateTime DateOfAdded { get; set; }
        public int IsActive { get; set; }
        //public ICollection<Product> Products { get; set; }
        public ICollection<PhotoForSupplier> PhotoForSuppliers { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Order> Order { get; set; }

    }
}