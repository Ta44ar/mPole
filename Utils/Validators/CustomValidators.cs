using System.ComponentModel.DataAnnotations;

namespace mPole.Utils.Validators
{
    public static class CustomValidators
    {
        public static ValidationResult? ValidateAge(DateTime dateOfBirth, ValidationContext context)
        {
            int age = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth > DateTime.Today.AddYears(-age)) age--;

            return age >= 18
                ? ValidationResult.Success
                : new ValidationResult("U�ytkownik musi mie� przynajmniej 18 lat.");
        }
    }
}
