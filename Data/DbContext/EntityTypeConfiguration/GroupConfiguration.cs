using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mPole.Data.Models;

namespace mPole.Data.DbContext.EntityTypeConfiguration
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Name)
                .IsRequired()
                .HasColumnType("nvarchar(50)");

            builder.Property(g => g.Level)
                .IsRequired()
                .HasColumnType("nvarchar(50)");

            builder.Property(g => g.RegularClassTime)
                .HasColumnType("datetime2");

            builder.Property(g => g.CapacityLimit)
                .IsRequired();

            builder.HasMany(g => g.Members)
                .WithMany(u => u.Groups)
                .UsingEntity<Dictionary<string, object>>(
                    "UserGroup",
                    j => j.HasOne<ApplicationUser>().WithMany().HasForeignKey("UserId"),
                    j => j.HasOne<Group>().WithMany().HasForeignKey("GroupId"));
        }
    }
}