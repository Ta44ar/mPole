using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using mPole.Data.DbContext.EntityTypeConfiguration;
using mPole.Data.Models;
using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;

namespace mPole_UnitTests_EntitiesConfiguration
{
    public class TrainingConfigurationTest
    {
        [Fact]
        public void Configure_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var modelBuilder = new ModelBuilder();
            var builder = modelBuilder.Entity<Training>();
            var configuration = new TrainingConfiguration();

            // Act
            configuration.Configure(builder);

            // Assert
            var idProperty = builder.Metadata.FindProperty(nameof(Training.Id));
            Assert.NotNull(idProperty);
            Assert.True(idProperty.IsPrimaryKey());

            var nameProperty = builder.Metadata.FindProperty(nameof(Training.Name));
            Assert.NotNull(nameProperty);
            Assert.False(nameProperty.IsNullable);
            Assert.Equal("nvarchar(50)", nameProperty.GetColumnType());

            var descriptionProperty = builder.Metadata.FindProperty(nameof(Training.Description));
            Assert.NotNull(descriptionProperty);
            Assert.False(descriptionProperty.IsNullable);
            Assert.Equal("nvarchar(1000)", descriptionProperty.GetColumnType());

            var typeProperty = builder.Metadata.FindProperty(nameof(Training.Type));
            Assert.NotNull(typeProperty);
            Assert.False(typeProperty.IsNullable);
            Assert.Equal("nvarchar(50)", typeProperty.GetColumnType());

            var levelProperty = builder.Metadata.FindProperty(nameof(Training.Level));
            Assert.NotNull(levelProperty);
            Assert.False(levelProperty.IsNullable);
            Assert.Equal("nvarchar(50)", levelProperty.GetColumnType());

            var imageUrlProperty = builder.Metadata.FindProperty(nameof(Training.ImageUrl));
            Assert.NotNull(imageUrlProperty);
            Assert.True(imageUrlProperty.IsNullable);

            // Check the many-to-many relationship with Move
            var movesNavigation = builder.Metadata.FindSkipNavigation(nameof(Training.Moves));
            Assert.NotNull(movesNavigation);
            Assert.Equal("MoveTraining", movesNavigation.JoinEntityType.Name);
            Assert.Equal("TrainingId", movesNavigation.ForeignKey.Properties[0].Name);
        }
    }
}