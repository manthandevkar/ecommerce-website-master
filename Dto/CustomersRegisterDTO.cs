using System.ComponentModel.DataAnnotations;
using CP.API.Validations;
namespace CP.API.Dto
{
    public class CustomerRegisterDTO
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required ")]
        [RegularExpression(" /^[^0-9]+$/", ErrorMessage = "First Name must Not Contains Number !!")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]

        [Required(ErrorMessage = "Last Name is required ")]
        [RegularExpression(" /^[^0-9]+$/", ErrorMessage = "Last Name must Not Contains Number !!")]
        public string LirstName { get; set; }//change
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{7})$",
                   ErrorMessage = "Entered phone format is not valid.")]
        public string Phone { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is  Required!")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [PasswordValidation]
        public string Password {get; set;}
    }
}