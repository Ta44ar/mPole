using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mPole.Data.Models;
using mPole.Data.DbContext.EntityTypeConfiguration;
using Moq;

namespace mPole_UnitTests_EntitiesConfiguration
{
    public class AppUserConfigurationTest
    {
        [Fact]
        public void Configure_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var modelBuilder = new ModelBuilder();
            var builder = modelBuilder.Entity<ApplicationUser>();
            var configuration = new AppUserConfiguration();

            // Act
            configuration.Configure(builder);

            // Assert
            var firstNameProperty = builder.Metadata.FindProperty(nameof(ApplicationUser.FirstName));
            Assert.NotNull(firstNameProperty);
            Assert.False(firstNameProperty.IsNullable);
            Assert.Equal("nvarchar(50)", firstNameProperty.GetColumnType());

            var lastNameProperty = builder.Metadata.FindProperty(nameof(ApplicationUser.LastName));
            Assert.NotNull(lastNameProperty);
            Assert.False(lastNameProperty.IsNullable);
            Assert.Equal("nvarchar(50)", lastNameProperty.GetColumnType());

            var profileImageProperty = builder.Metadata.FindProperty(nameof(ApplicationUser.ProfileImage));
            Assert.NotNull(profileImageProperty);
            Assert.Equal("varbinary(max)", profileImageProperty.GetColumnType());
        }
    }
}