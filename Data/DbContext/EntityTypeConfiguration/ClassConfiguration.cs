using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mPole.Data.Models;

namespace mPole.Data.DbContext.EntityTypeConfiguration
{
    public class ClassConfiguration : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Duration)
                .IsRequired()
                .HasColumnType("nvarchar(50)");

            builder.Property(c => c.Date)
                .IsRequired();

            builder.Property(c => c.Location)
                .IsRequired()
                .HasColumnType("nvarchar(50)");

            builder.Property(c => c.TrainerId)
                .IsRequired()
                .HasColumnType("nvarchar(450)");

            builder.HasOne(c => c.Training)
                .WithMany(t => t.Classes)
                .HasForeignKey(c => c.TrainingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.RegisteredUsers)
                .WithMany(u => u.Classes)
                .UsingEntity<Dictionary<string, object>>(
                    "UserClass",
                    j => j.HasOne<ApplicationUser>().WithMany().HasForeignKey("UserId"),
                    j => j.HasOne<Class>().WithMany().HasForeignKey("ClassId"));

            builder.HasOne(c => c.Trainer)
                .WithMany(u => u.InstructedClasses)
                .HasForeignKey(c => c.TrainerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
