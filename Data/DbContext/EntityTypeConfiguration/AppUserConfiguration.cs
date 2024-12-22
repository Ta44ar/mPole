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

            builder.HasMany(e => e.Classes)
                .WithMany(c => c.RegisteredUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "UserClass",
                    j => j.HasOne<Class>().WithMany().HasForeignKey("ClassId"),
                    j => j.HasOne<ApplicationUser>().WithMany().HasForeignKey("UserId"));
        }
    }
}
