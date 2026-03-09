using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_learning.Core.Entities.AdminOperations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_learning.Repository.Config.AdminOperationsConfiguration
{
    public class PayoutApprovalsConfiguration : IEntityTypeConfiguration<PayoutApprovals>
    {

        public void Configure(EntityTypeBuilder<PayoutApprovals> builder)
        {
            builder.ToTable("PayoutApprovals");

            // Primary Key
            builder.HasKey(p => p.Id);

            // Properties
            builder.Property(p => p.Decision)
                   .IsRequired();

            builder.Property(p => p.Notes)
                   .HasMaxLength(1000);

            builder.Property(p => p.ProcessedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Relationship: PayoutApprovals -> PayoutRequest
            builder.HasOne(p => p.PayoutRequest)
                   .WithMany(r => r.PayoutApprovals)
                   .HasForeignKey(p => p.PayoutRequestId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationship: PayoutApprovals -> Admin
            builder.HasOne(p => p.Admin)
                   .WithMany(a => a.PayoutApprovals)
                   .HasForeignKey(p => p.AdminId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(p => p.PayoutRequestId);

            builder.HasIndex(p => p.AdminId);

            // Prevent same admin approving same request twice
            builder.HasIndex(p => new { p.PayoutRequestId, p.AdminId })
                   .IsUnique();
        }



    
    }
}