using CaseManagement.Api.Domain.Entities;
using CaseManagement.Api.Entities;
using CaseManagement.Api.Infrastructure.Data.Configurations;
using CaseManagement.Api.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;

namespace CaseManagement.Api.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options, PasswordHasher hasher) : DbContext(options)
    {
        private PasswordHasher _hasher = hasher;

        public DbSet<User> User => Set<User>();
        public DbSet<Officer> Officer => Set<Officer>();
        public DbSet<Client> Client => Set<Client>();
        public DbSet<Case> Case => Set<Case>();
        public DbSet<Domain.Entities.ServiceProvider> ServiceProvider => Set<Domain.Entities.ServiceProvider>();
        public DbSet<AuditLog> AuditLog => Set<AuditLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<AuditLog>(entity =>
            // {
            //     entity.ToTable("AuditLog");

            //     entity.HasKey(e => e.Id);

            //     entity.Property(e => e.EntityName)
            //             .IsRequired()
            //             .HasMaxLength(100);

            //     entity.Property(e => e.Action)
            //             .IsRequired()
            //             .HasMaxLength(50);

            //     entity.Property(e => e.Username)
            //             .HasMaxLength(100);

            //     entity.Property(e => e.UserRole)
            //             .HasMaxLength(50);

            //     entity.Property(e => e.TimestampUtc)
            //             .HasDefaultValueSql("SYSUTCDATETIME()");
            // });

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            // modelBuilder.ApplyConfiguration(new CaseConfiguration());

            //modelBuilder.Entity<User>().HasData(
            //    new User
            //    {
            //        Id = Guid.Parse("..."),
            //        Username = "admin",
            //        PasswordHash = _hasher.Hash("Admin123!"),
            //        Role = "Admin"
            //    }
            //);

        }

    }

}