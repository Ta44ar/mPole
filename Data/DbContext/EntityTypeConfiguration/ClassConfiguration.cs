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

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)");

            builder.Property(c => c.Location)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nvarchar(50)");

            builder.Property(c => c.Duration)
                .IsRequired();

            builder.Property(c => c.Date)
                .IsRequired(false);

            builder.Property(c => c.Time)
                .IsRequired(false);

            builder.Property(c => c.IsRegistrationOpen)
                .IsRequired();

            builder.HasOne(c => c.Training)
                .WithMany(t => t.Classes)
                .HasForeignKey(c => c.TrainingId);

            builder.HasOne(c => c.Trainer)
                .WithMany(u => u.InstructedClasses)
                .HasForeignKey(c => c.TrainerId);

            builder.HasMany(c => c.Registrations)
                .WithOne(r => r.Class)
                .HasForeignKey(r => r.ClassId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Trainer)
                .WithMany(u => u.InstructedClasses)
                .HasForeignKey(c => c.TrainerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}