using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mPole.Data.DbContext.EntityTypeConfiguration;
using mPole.Data.Models;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace mPole_UnitTests_EntitiesConfiguration
{
    public class ImageConfigurationTest
    {
        [Fact]
        public void Configure_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var modelBuilder = new ModelBuilder();
            var builder = modelBuilder.Entity<Image>();
            var configuration = new ImageConfiguration();

            // Act
            configuration.Configure(builder);

            // Assert
            var idProperty = builder.Metadata.FindProperty(nameof(Image.Id));
            Assert.NotNull(idProperty);
            Assert.True(idProperty.IsPrimaryKey());

            var nameProperty = builder.Metadata.FindProperty(nameof(Image.Name));
            Assert.NotNull(nameProperty);
            Assert.False(nameProperty.IsNullable);
            Assert.Equal("nvarchar(50)", nameProperty.GetColumnType());

            var imageDataProperty = builder.Metadata.FindProperty(nameof(Image.ImageData));
            Assert.NotNull(imageDataProperty);
            Assert.False(imageDataProperty.IsNullable);
            Assert.Equal("varbinary(max)", imageDataProperty.GetColumnType());
        }
    }
}