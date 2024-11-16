using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using mPole.Data.Models;

namespace mPole.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<PoleDanceMove> PoleDanceMoves { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<PoleDanceMoveImage> PoleDanceMoveImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "User");
            });
            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRole");
            });
            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaim");
            });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogin");
            });
            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserToken");
            });
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaim");
            });

            modelBuilder.Entity<PoleDanceMove>(entity =>
            {
                entity.ToTable(name: "PoleDanceMove");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable(name: "Image");
            });

            modelBuilder.Entity<PoleDanceMoveImage>(entity =>
            {
                entity.ToTable(name: "PoleDanceMoveImage");
                entity.HasKey(e => new { e.PoleDanceMoveId, e.ImageId });
                entity.HasOne(e => e.PoleDanceMove)
                    .WithMany(e => e.PoleDanceMoveImages)
                    .HasForeignKey(e => e.PoleDanceMoveId);
                entity.HasOne(e => e.Image)
                    .WithMany(e => e.PoleDanceMoveImages)
                    .HasForeignKey(e => e.ImageId);
            });
        }
    }
}
