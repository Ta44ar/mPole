using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mPole.Data.Models;

namespace mPole.Data.DbContext.EntityTypeConfiguration
{
    public class TrainingConfiguration : IEntityTypeConfiguration<Training>
    {
        public void Configure(EntityTypeBuilder<Training> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasColumnType("nvarchar(50)");

            builder.Property(t => t.Description)
                .IsRequired()
                .HasColumnType("nvarchar(1000)");

            builder.Property(t => t.Type)
                .IsRequired()
                .HasColumnType("nvarchar(50)");

            builder.Property(t => t.Level)
                .IsRequired()
                .HasColumnType("nvarchar(50)");

            builder.Property(t => t.ImageUrl)
                .IsRequired(false);

            builder.HasMany(t => t.Moves)
                .WithMany(m => m.Trainings)
                .UsingEntity<Dictionary<string, object>>(
                    "MoveTraining",
                    j => j.HasOne<Move>().WithMany().HasForeignKey("MoveId"),
                    j => j.HasOne<Training>().WithMany().HasForeignKey("TrainingId"));
        }
    }
}
