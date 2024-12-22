using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mPole.Data.Models;

namespace mPole.Data.DbContext.EntityTypeConfiguration
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name)
                .IsRequired()
                .HasColumnType("nvarchar(50)");

            builder.Property(i => i.ImageData)
                .IsRequired()
                .HasColumnType("varbinary(max)");
        }
    }
}
