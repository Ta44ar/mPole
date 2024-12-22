using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mPole.Data.Models;

namespace mPole.Data.DbContext.EntityTypeConfiguration
{
    public class MoveConfiguration : IEntityTypeConfiguration<Move>
    {
        public void Configure(EntityTypeBuilder<Move> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name)
                .IsRequired()
                .HasColumnType("nvarchar(50)");

            builder.Property(m => m.Description)
                .IsRequired()
                .HasColumnType("nvarchar(1000)");

            builder.Property(m => m.DifficultyLevel)
                .IsRequired();

            builder.HasMany(m => m.Images)
                .WithOne(i => i.Move)
                .HasForeignKey(i => i.MoveId);

            builder.HasMany(m => m.Trainings)
                .WithMany(t => t.Moves)
                .UsingEntity<Dictionary<string, object>>(
                    "MoveTraining",
                    j => j.HasOne<Training>().WithMany().HasForeignKey("TrainingId"),
                    j => j.HasOne<Move>().WithMany().HasForeignKey("MoveId"));
        }
    }
}
