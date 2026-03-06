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
    public class PaymentTransactionsConfiguration : IEntityTypeConfiguration<PaymentTransactions>
    {
        public void Configure(EntityTypeBuilder<PaymentTransactions> builder)
        {
            // Table
            builder.ToTable("PaymentTransactions");

            // Primary Key
            builder.HasKey(p => p.Id);


            // Properties


            builder.Property(p => p.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.Currency)
                .HasMaxLength(10)
                .HasDefaultValue("USD");

            builder.Property(p => p.Status)
                .HasConversion<int>()
                .HasDefaultValue(PaymentTransactionsStatus.Pending);

            builder.Property(p => p.GatewayReference)
                .HasMaxLength(200);

            builder.Property(p => p.FailureReason)
                .HasMaxLength(500);

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(p => p.CompletedAt)
                .IsRequired(false);


            // Relationships


            // Student → PaymentTransactions (1 : many)
            builder.HasOne(p => p.Student)
                .WithMany(u => u.PaymentTransactions)
                .HasForeignKey(p => p.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Course → PaymentTransactions (1 : many)
            builder.HasOne(p => p.Courses)
                .WithMany(c => c.PaymentTransactions)
                .HasForeignKey(p => p.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // PaymentMethod → PaymentTransactions (1 : many)
            builder.HasOne(p => p.PaymentMethods)
                .WithMany(pm => pm.PaymentTransactions)
                .HasForeignKey(p => p.PaymentMethodId)
                .OnDelete(DeleteBehavior.Restrict);

            // PaymentTransactions → InstructorEarnings (1 : many)
            builder.HasMany(p => p.InstructorEarnings)
                .WithOne(e => e.PaymentTransactions)
                .HasForeignKey(e => e.TransactionId)
                .OnDelete(DeleteBehavior.Restrict);


            // Indexes (Recommended)


            builder.HasIndex(p => p.StudentId);
            builder.HasIndex(p => p.CourseId);
            builder.HasIndex(p => p.Status);
            builder.HasIndex(p => p.CreatedAt);
        }
    }
}
