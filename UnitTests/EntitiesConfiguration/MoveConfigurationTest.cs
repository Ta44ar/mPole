using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using mPole.Data.DbContext.EntityTypeConfiguration;
using mPole.Data.Models;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace mPole_UnitTests_EntitiesConfiguration
{
    public class MoveConfigurationTest
    {
        [Fact]
        public void Configure_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var modelBuilder = new ModelBuilder();
            var builder = modelBuilder.Entity<Move>();
            var configuration = new MoveConfiguration();

            // Act
            configuration.Configure(builder);

            // Assert
            var idProperty = builder.Metadata.FindProperty(nameof(Move.Id));
            Assert.NotNull(idProperty);
            Assert.True(idProperty.IsPrimaryKey());

            var nameProperty = builder.Metadata.FindProperty(nameof(Move.Name));
            Assert.NotNull(nameProperty);
            Assert.False(nameProperty.IsNullable);
            Assert.Equal("nvarchar(50)", nameProperty.GetColumnType());

            var descriptionProperty = builder.Metadata.FindProperty(nameof(Move.Description));
            Assert.NotNull(descriptionProperty);
            Assert.False(descriptionProperty.IsNullable);
            Assert.Equal("nvarchar(1000)", descriptionProperty.GetColumnType());

            var difficultyLevelProperty = builder.Metadata.FindProperty(nameof(Move.DifficultyLevel));
            Assert.NotNull(difficultyLevelProperty);
            Assert.False(difficultyLevelProperty.IsNullable);

            var imagesNavigation = builder.Metadata.FindNavigation(nameof(Move.Images));
            Assert.NotNull(imagesNavigation);
            Assert.Equal("MoveId", imagesNavigation.ForeignKey.Properties[0].Name);

            // Check the join table for the many-to-many relationship
            var trainingsNavigation = builder.Metadata.FindSkipNavigation(nameof(Move.Trainings));
            Assert.NotNull(trainingsNavigation);
            Assert.Equal("MoveTraining", trainingsNavigation.JoinEntityType.Name);
            Assert.Equal("MoveId", trainingsNavigation.ForeignKey.Properties[0].Name);
        }
    }
}