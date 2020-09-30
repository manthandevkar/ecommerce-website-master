using System;
using System.Collections.Generic;
using CP.API.Model;
using System.ComponentModel.DataAnnotations;
using CP.API.Validations;

namespace CP.API.Dto
{
    public class UserRegisterDTO
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User Name is required ")]
        //[RegularExpression(" /^[^0-9]+$/", ErrorMessage = "First Name must Not Contains Number !!")]
        public string UserName { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{7})$",
               ErrorMessage = "Entered phone format is not valid.")]
        public string Phone { get; set; }
        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country is required ")]
       // [RegularExpression(" /^[^0-9]+$/", ErrorMessage = "First Name must Not Contains Number !!")]
        public string Country { get; set; }
        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is required ")]
       // [RegularExpression(" /^[^0-9]+$/", ErrorMessage = "Address must Not Contains Number !!")]
        public string Address { get; set; }
        public string City { get; set; }
        public DateTime DateOfAdded { get; set; }
        public int IsActive { get; set; } = 0;
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is  Required!")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [PasswordValidation]
        public string Password { get; set; }
        public UserRegisterDTO()
        {
            DateOfAdded = DateTime.Now;
        }

    }
}