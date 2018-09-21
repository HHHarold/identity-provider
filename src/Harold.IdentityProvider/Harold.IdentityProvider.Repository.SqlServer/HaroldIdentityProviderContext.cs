using Harold.IdentityProvider.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Harold.IdentityProvider.Repository.SqlServer
{
    public partial class HaroldIdentityProviderContext : DbContext
    {
        public HaroldIdentityProviderContext()
        {
        }

        public HaroldIdentityProviderContext(DbContextOptions<HaroldIdentityProviderContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Logins> Logins { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-D5CP97M\\SQLEXPRESS;Database=HaroldIdentityProvider;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Logins>(entity =>
            {
                entity.HasKey(e => e.LoginId);

                entity.HasIndex(e => e.UserName)
                    .HasName("AK_Logins_UserName")
                    .IsUnique();

                entity.Property(e => e.LoginId).ValueGeneratedNever();

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Logins_Roles");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Logins_Users");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.RolId);

                entity.Property(e => e.RolId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(60);
            });
        }
    }
}
