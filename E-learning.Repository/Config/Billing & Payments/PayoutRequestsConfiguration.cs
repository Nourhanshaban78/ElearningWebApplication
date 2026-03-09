using E_learning.Core.Entities.Billing___Payments;
using E_learning.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Repository.Config.Billing___Payments
{
    public class PayoutRequestsConfiguration : IEntityTypeConfiguration<PayoutRequest>
    {
        public void Configure(EntityTypeBuilder<PayoutRequest> builder)
        {
            builder.ToTable("PayoutRequests");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(x => x.Method)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.AccountDetails)
                   .HasMaxLength(500);

            builder.Property(x => x.Status)
                   .IsRequired()
                   .HasDefaultValue(PaymentTransactionsStatus.Pending);

            builder.Property(x => x.RequestedAt)
                   .IsRequired();

            builder.Property(x => x.ProcessedAt);

            builder.Property(x => x.AdminNotes)
                   .HasMaxLength(1000);

            // Instructor Relation
            builder.HasOne(x => x.Instructor)
                   .WithMany(i => i.PayoutRequests)
                   .HasForeignKey(x => x.InstructorId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Approvals Relation
            builder.HasMany(x => x.PayoutApprovals)
                   .WithOne(a => a.PayoutRequest)
                   .HasForeignKey(a => a.PayoutRequestId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Index for faster instructor queries
            builder.HasIndex(x => x.InstructorId);
            builder.HasIndex(x => x.Status);
        
    }
    }
    }
