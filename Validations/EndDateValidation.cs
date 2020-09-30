using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using CP.API.Dto;
using CP.API.Model;

namespace CP.API.Validations
{
    public class EndDateValidation :ValidationAttribute
    {
        Coupon obj = new Coupon();

        //CouponRegisterDTO obj = new CouponRegisterDTO();
        //start date must  not be less than current time
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (Convert.ToDateTime(value) < DateTime.Now  || Convert.ToDateTime(value) <obj.StartDate)
            {
                return new ValidationResult(base.ErrorMessage);
            }
            return ValidationResult.Success;

        }

    }
}
