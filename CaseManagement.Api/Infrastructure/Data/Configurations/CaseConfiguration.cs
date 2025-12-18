using CaseManagement.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaseManagement.Api.Infrastructure.Data.Configurations
{
    public class CaseConfiguration : IEntityTypeConfiguration<Case>
    {
        public void Configure(EntityTypeBuilder<Case> builder)
        {
            builder.ToTable("Case");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Region)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Recommendations)
                .HasColumnType("nvarchar(max)");

            builder.Property(c => c.CreatedAt)
                .HasDefaultValueSql("SYSUTCDATETIME()");

            builder.HasIndex(x => x.ClientId);
            builder.HasIndex(x => x.AssignedOfficerId);

            builder.HasOne(c => c.Client)
                .WithMany()
                .HasForeignKey(c => c.ClientId);

            builder.HasOne(c => c.AssignedOfficer)
                .WithMany()
                .HasForeignKey(c => c.AssignedOfficerId);

            builder.HasOne(c => c.ServiceProvider)
                .WithMany()
                .HasForeignKey(c => c.ServiceProviderId);
        }
    }
}