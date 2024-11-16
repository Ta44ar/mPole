using Microsoft.AspNetCore.Identity;
using mPole.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace mPole.Data.Validators
{
    public static class CustomValidators
    {
        public static ValidationResult? ValidateAge(DateTime dateOfBirth, ValidationContext context)
        {
            int age = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth > DateTime.Today.AddYears(-age)) age--;

            return age >= 18
                ? ValidationResult.Success
                : new ValidationResult("U¿ytkownik musi mieæ przynajmniej 18 lat.");
        }
    }
}
