using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace CP.API.Validations
{
    public class StartDateValidation:ValidationAttribute
    {

        //start date must  not be less than current time
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (Convert.ToDateTime(value) < DateTime.Now)
            {
                return new ValidationResult(base.ErrorMessage);
            }
            return ValidationResult.Success;

        }

    }
}
