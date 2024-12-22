using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using mPole.Data.DbContext.EntityTypeConfiguration;
using mPole.Data.Models;

namespace mPole.Data.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Move> Moves { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Class> Classes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.ApplyConfiguration(new MoveConfiguration());
            modelBuilder.ApplyConfiguration(new ImageConfiguration());
            modelBuilder.ApplyConfiguration(new TrainingConfiguration());
            modelBuilder.ApplyConfiguration(new ClassConfiguration());

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "User");
                entity.Property(e => e.ProfileImage).HasColumnType("varbinary(max)");
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

            modelBuilder.Entity<Move>(entity =>
            {
                entity.ToTable(name: "Move");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable(name: "Image");
            });

            modelBuilder.Entity<Training>(entity =>
            {
                entity.ToTable(name: "Training");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable(name: "Class");
            });
        }
    }
}
