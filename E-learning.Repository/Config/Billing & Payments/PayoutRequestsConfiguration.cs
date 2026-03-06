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
    public class PayoutRequestsConfiguration : IEntityTypeConfiguration<PayoutRequests>
    {
        public void Configure(EntityTypeBuilder<PayoutRequests> builder)
        {
            // Table
            builder.ToTable("PayoutRequests");

            // Primary Key
            builder.HasKey(p => p.Id);


            // Properties


            builder.Property(p => p.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.Method)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.AccountDetails)
                .HasMaxLength(500);

            builder.Property(p => p.Status)
                .HasConversion<int>()
                .HasDefaultValue(PaymentTransactionsStatus.Pending);

            builder.Property(p => p.AdminNotes)
                .HasMaxLength(1000);

            builder.Property(p => p.RequestedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(p => p.ProcessedAt)
                .IsRequired(false);


            // Relationships


            // Instructor → PayoutRequests (1 : many)
            builder.HasOne(p => p.Instructor)
                .WithMany(u => u.PayoutRequests)
                .HasForeignKey(p => p.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);


            // Indexes (Recommended)


            builder.HasIndex(p => p.InstructorId);
            builder.HasIndex(p => p.Status);
            builder.HasIndex(p => p.RequestedAt);
        }
    }
    }
