using Xunit;
using Microsoft.EntityFrameworkCore;
using mPole.Data.Models;
using mPole.Data.DbContext.EntityTypeConfiguration;

namespace mPole_UnitTests_EntitiesConfiguration
{
    public class ClassConfigurationTest
    {
        [Fact]
        public void Configure_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var modelBuilder = new ModelBuilder();
            var builder = modelBuilder.Entity<Class>();
            var configuration = new ClassConfiguration();

            // Act
            configuration.Configure(builder);

            // Assert
            var durationProperty = builder.Metadata.FindProperty(nameof(Class.Duration));
            Assert.NotNull(durationProperty);
            Assert.False(durationProperty.IsNullable);

            var dateProperty = builder.Metadata.FindProperty(nameof(Class.Date));
            Assert.NotNull(dateProperty);
            Assert.True(dateProperty.IsNullable);

            var timeProperty = builder.Metadata.FindProperty(nameof(Class.Time));
            Assert.NotNull(timeProperty);
            Assert.True(timeProperty.IsNullable);

            var locationProperty = builder.Metadata.FindProperty(nameof(Class.Location));
            Assert.NotNull(locationProperty);
            Assert.False(locationProperty.IsNullable);
            Assert.Equal("nvarchar(100)", locationProperty.GetColumnType());

            var trainerIdProperty = builder.Metadata.FindProperty(nameof(Class.TrainerId));
            Assert.NotNull(trainerIdProperty);
            Assert.False(trainerIdProperty.IsNullable);
            Assert.Equal("nvarchar(450)", trainerIdProperty.GetColumnType());

            var isRegistrationOpenProperty = builder.Metadata.FindProperty(nameof(Class.IsRegistrationOpen));
            Assert.NotNull(isRegistrationOpenProperty);
            Assert.False(isRegistrationOpenProperty.IsNullable);
        }
    }
}