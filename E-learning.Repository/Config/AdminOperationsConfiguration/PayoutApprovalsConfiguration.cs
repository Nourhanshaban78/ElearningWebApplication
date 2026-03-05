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

            builder.HasKey(i => i.Id);

            builder.HasOne(p => p.PayoutRequest)
                 .WithOne()
                 .HasForeignKey<PayoutApprovals>(p => p.PayoutRequestId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Admin)
                 .WithMany()
                 .HasForeignKey(p => p.AdminId)
                 .OnDelete(DeleteBehavior.Cascade);
     
            builder.Property(i => i.Decision)
                  .HasConversion<string>()
                  .HasMaxLength(20);

            builder.Property(i => i.Notes)
                   .HasMaxLength(500);

            builder.Property(us => us.ProcessedAt)
                   .HasDefaultValueSql("GETUTCDATE()");



        }
    }
}