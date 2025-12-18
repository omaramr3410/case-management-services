using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceProvider = CaseManagement.Api.Domain.Entities.ServiceProvider;

namespace CaseManagement.Api.Infrastructure.Data.Configurations
{
    public class ServiceProviderConfiguration : IEntityTypeConfiguration<ServiceProvider>
    {
        public void Configure(EntityTypeBuilder<ServiceProvider> builder)
        {
            builder.ToTable("ServiceProvider");

            builder.HasKey(sp => sp.Id);

            builder.Property(sp => sp.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(sp => sp.Region)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(sp => sp.ServiceType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(sp => sp.CreatedAt)
                .HasDefaultValueSql("SYSUTCDATETIME()");

            builder.HasIndex(sp => sp.Region);
        }
    }
}