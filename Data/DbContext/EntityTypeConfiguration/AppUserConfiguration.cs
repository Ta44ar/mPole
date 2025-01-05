using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mPole.Data.Models;

namespace mPole.Data.DbContext.EntityTypeConfiguration
{
    public class AppUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasColumnType("nvarchar(50)");

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasColumnType("nvarchar(50)");

            builder.Property(e => e.ProfileImage)
                .HasColumnType("varbinary(max)");

            builder.HasMany(e => e.Groups)
                .WithMany(g => g.Members)
                .UsingEntity<Dictionary<string, object>>(
                    "UserGroup",
                    j => j.HasOne<Group>().WithMany().HasForeignKey("GroupId"),
                    j => j.HasOne<ApplicationUser>().WithMany().HasForeignKey("UserId"));

            builder.HasMany(e => e.InstructedClasses)
                .WithOne(c => c.Trainer)
                .HasForeignKey(c => c.TrainerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}