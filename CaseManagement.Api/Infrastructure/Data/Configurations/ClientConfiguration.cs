using CaseManagement.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaseManagement.Api.Infrastructure.Data.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Region)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.SSN)
                .IsRequired()
                .HasMaxLength(11);

            builder.Property(c => c.DateOfBirth)
                .IsRequired();

            builder.Property(c => c.Phone)
                .HasMaxLength(20);

            builder.Property(c => c.Address)
                .HasMaxLength(200);

            builder.Property(c => c.CreatedAt)
                .HasDefaultValueSql("SYSUTCDATETIME()");

            builder.HasIndex(c => c.SSN).IsUnique();
            builder.HasIndex(c => c.Region);
        }
    }
}