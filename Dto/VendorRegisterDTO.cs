using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace CP.API.Dto
{
    public class VendorRegisterDTO
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User Name is required ")]
        [RegularExpression(" /^[^0-9]+$/", ErrorMessage = "First Name must Not Contains Number !!")]
        public string UserName { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{7})$",
        ErrorMessage = "Entered phone format is not valid.")]
        public string Phone { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string Country { get; set; }
        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is required ")]
        [RegularExpression(" /^[^0-9]+$/", ErrorMessage = "Address must Not Contains Number !!")]
        public string Address { get; set; }
        public string City { get; set; }
        public int IsActive { get; set; } = 0;
        public DateTime DateOfAdded { get; set; }

        public VendorRegisterDTO()
        {
            DateOfAdded = DateTime.Now;
        }
    }
}
