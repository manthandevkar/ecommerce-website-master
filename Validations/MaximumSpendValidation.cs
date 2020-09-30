using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using CP.API.Dto;
using CP.API.Model;
namespace CP.API.Validations
{
    public class MaximumSpendValidation: ValidationAttribute
    {

        Coupon obj = new Coupon();
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (Convert.ToInt32(value) < obj.MinimumSpend)
            {
                return new ValidationResult(base.ErrorMessage);
            }
            return ValidationResult.Success;

        }

    }
}
