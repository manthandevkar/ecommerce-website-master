using System.Collections.Generic;
using CP.API.Model;
using System.ComponentModel.DataAnnotations;

namespace CP.API.Dto
{
    public class ShipperRegisterDTO
    {
        public int ShipperId { get; set; }
        [RegularExpression(" /^[^0-9]+$/", ErrorMessage = "CompanyName  must Not Contains Number !!")]

        public string CompanyName { get; set; }
        public string Phone { get; set; }
    }
}