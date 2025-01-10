using Xunit;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using mPole.Data.DTOs;

namespace mPole_UnitTests_AuthValidation
{
    public class RegisterUserDtoTests
    {
        [Fact]
        public void RegisterUserDto_ShouldHaveValidProperties()
        {
            // Arrange
            var dto = new RegisterUserDto
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "jan.kowalski@example.com",
                Password = "Password123!",
                ConfirmPassword = "Password123!",
                DateOfBirth = new DateTime(1990, 1, 1)
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(dto, null, null);
            var isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void RegisterUserDto_ShouldFailValidation_WhenPropertiesAreInvalid()
        {
            // Arrange
            var dto = new RegisterUserDto
            {
                FirstName = "J",
                LastName = "K",
                Email = "invalid-email",
                Password = "short",
                ConfirmPassword = "different"
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(dto, null, null);
            var isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("Imi� musi zawiera� 2-20 znak�w."));
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("Nazwisko musi zawiera� 2-20 znak�w."));
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("Adres email nieprawid�owy."));
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("Has�o musi mie� przynajmniej 8 i maksymalnie 20 znak�w."));
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("Oba has�a musz� by� identyczne."));
        }
    }
}