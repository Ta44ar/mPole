using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mPole.Data.Models;

namespace mPole.Data
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

    public class TrainingConfiguration : IEntityTypeConfiguration<Training>
    {
        public void Configure(EntityTypeBuilder<Training> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasColumnType("nvarchar(50)");

            builder.Property(t => t.Description)
                .IsRequired()
                .HasColumnType("nvarchar(1000)");

            builder.Property(t => t.Type)
                .IsRequired()
                .HasColumnType("nvarchar(50)");

            builder.Property(t => t.ImageUrl)
                .IsRequired(false);

            builder.HasMany(t => t.Moves)
                .WithMany(m => m.Trainings)
                .UsingEntity<Dictionary<string, object>>(
                    "MoveTraining",
                    j => j.HasOne<Move>().WithMany().HasForeignKey("MoveId"),
                    j => j.HasOne<Training>().WithMany().HasForeignKey("TrainingId"));
        }
    }

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

            builder.Property(c => c.Trainer)
                .IsRequired()
                .HasColumnType("nvarchar(50)");

            builder.HasOne(c => c.Training)
                .WithMany(t => t.Classes)
                .HasForeignKey(c => c.TrainingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(c => c.RegisteredUsers)
                .IsRequired()
                .HasColumnType("nvarchar(max)");
        }
    }
}
