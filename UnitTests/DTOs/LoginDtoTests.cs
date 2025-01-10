using Xunit;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using mPole.Data.DTOs;

namespace mPole_UnitTests_AuthValidation
{
    public class LoginDtoTests
    {
        [Fact]
        public void LoginDto_ShouldHaveValidProperties()
        {
            // Arrange
            var dto = new LoginDto
            {
                Email = "jan.kowalski@example.com",
                Password = "Password123!",
                RememberMe = true
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(dto, null, null);
            var isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void LoginDto_ShouldFailValidation_WhenPropertiesAreInvalid()
        {
            // Arrange
            var dto = new LoginDto
            {
                Email = "invalid-email",
                Password = ""
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(dto, null, null);
            var isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("Nieprawid³owy adres email."));
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("Has³o jest wymagane."));
        }
    }
}