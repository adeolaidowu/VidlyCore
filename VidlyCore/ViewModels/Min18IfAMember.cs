using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VidlyCore.Models;

namespace VidlyCore.ViewModels
{
    public class Min18IfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;
            if (customer.MembershipTypeId == MembershipType.Unknown || customer.MembershipTypeId == MembershipType.PayAsYouGo)
                return ValidationResult.Success;
            if (customer.BirthDate == null)
                return new ValidationResult("Birth Date is required");
            var age = DateTime.Today.Year - customer.BirthDate.Value.Year;
            return age >= 18 ?
                ValidationResult.Success :
                new ValidationResult("Customer must be at least 18 years old to go on a membership"); 
        }
    }
}
