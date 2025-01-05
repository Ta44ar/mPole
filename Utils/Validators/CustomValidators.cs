using FluentValidation;
using MudBlazor;
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
                : new ValidationResult("U¿ytkownik musi mieæ przynajmniej 18 lat.");
        }

        public static async Task<bool> ValidateAndShowErrors<T>(IValidator<T> validator, T instance, ISnackbar snackbar)
        {
            var validationResult = await validator.ValidateAsync(instance);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    snackbar.Add(error.ErrorMessage, MudBlazor.Severity.Error);
                }
                return false;
            }
            return true;
        }
    }
}
