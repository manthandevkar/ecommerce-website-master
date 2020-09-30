using System;
using System.Collections.Generic;

namespace CP.API.Dto
{
    public class UserReturnDTO
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public DateTime DateOfAdded { get; set; }
        public int IsActive { get; set; }
        public string PhotoURL { get; set; }
        public ICollection<ProductReturnDTO> Products { get; set; }
        public ICollection<PhotoForReturnDto> Photos { get; set; }
    }
}
