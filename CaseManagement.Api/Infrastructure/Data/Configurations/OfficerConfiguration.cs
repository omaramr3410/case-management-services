using CaseManagement.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaseManagement.Api.Infrastructure.Data.Configurations
{
    public class OfficerConfiguration : IEntityTypeConfiguration<Officer>
    {
        public void Configure(EntityTypeBuilder<Officer> builder)
        {
            builder.ToTable("Officer");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.Region)
                .IsRequired()
                .HasMaxLength(50);

            // Foreign key to User table
            builder.Property(o => o.UserId)
                .IsRequired();

            builder.HasIndex(o => o.UserId)
                .HasDatabaseName("IX_Officer_UserId");

            builder.HasIndex(o => o.Region)
                .HasDatabaseName("IX_Officer_Region");
        }
    }
}
