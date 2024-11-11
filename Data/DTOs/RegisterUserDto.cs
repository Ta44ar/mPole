using System.ComponentModel.DataAnnotations;

namespace mPole.Data.DTOs
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "Imię jest wymagane.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Imię nie może być dłuższe niż 20 znaków.")]
        [RegularExpression(@"^[A-ZĄĆĘŁŃÓŚŹŻ][a-ząćęłńóśźż]*$", ErrorMessage = "Imię musi zaczynać się wielką literą.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Nazwisko nie może być dłuższe niż 20 znaków.")]
        [RegularExpression(@"^[A-ZĄĆĘŁŃÓŚŹŻ][a-ząćęłńóśźż]*$", ErrorMessage = "Nazwisko musi zaczynać się wielką literą.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adres email jest wymagany.")]
        [EmailAddress(ErrorMessage = "Adres email nieprawidłowy.")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [StringLength(20, ErrorMessage = "Hasło musi mieć przynajmniej {2} i maksymalnie {1} znaków.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Oba hasła muszą być identyczne.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Data urodzenia jest wymagana.")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(RegisterUserDto), nameof(ValidateAge))]
        public DateTime DateOfBirth { get; set; }

        public static ValidationResult? ValidateAge(DateTime dateOfBirth, ValidationContext context)
        {
            int age = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > DateTime.Today.AddYears(-age)) age--;

            return age >= 18
                ? ValidationResult.Success
                : new ValidationResult("Użytkownik musi mieć przynajmniej 18 lat.");
        }
    }
}
