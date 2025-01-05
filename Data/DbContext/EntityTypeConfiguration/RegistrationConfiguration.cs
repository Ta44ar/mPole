using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mPole.Data.Models;

namespace mPole.Data.DbContext.EntityTypeConfiguration
{
    public class RegistrationConfiguration : IEntityTypeConfiguration<Registration>
    {
        public void Configure(EntityTypeBuilder<Registration> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.UserId)
                .IsRequired();

            builder.Property(r => r.ClassId)
                .IsRequired();

            builder.Property(r => r.RegistrationDate)
                .IsRequired();

            builder.Property(r => r.Status)
                .IsRequired();

            builder.HasOne(r => r.User)
                .WithMany(u => u.Registrations)
                .HasForeignKey(r => r.UserId);

            builder.HasOne(r => r.Class)
                .WithMany(c => c.Registrations)
                .HasForeignKey(r => r.ClassId);
        }
    }
}