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
    public class PaymentTransactionsConfiguration : IEntityTypeConfiguration<PaymentTransaction>
    {
        public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
        {
            builder.ToTable("PaymentTransactions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(x => x.Currency)
                   .HasMaxLength(10)
                   .IsRequired()
                   .HasDefaultValue("USD");

            builder.Property(x => x.Status)
                   .IsRequired();

            builder.Property(x => x.GatewayReference)
                   .HasMaxLength(200);

            builder.Property(x => x.FailureReason)
                   .HasMaxLength(500);

            builder.Property(x => x.CreatedAt)
                   .IsRequired();

            builder.Property(x => x.CompletedAt);

            // Student Relation
            builder.HasOne(x => x.Student)
                   .WithMany(s => s.PaymentTransactions)
                   .HasForeignKey(x => x.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Course Relation
            builder.HasOne(x => x.Course)
                   .WithMany(c => c.PaymentTransactions)
                   .HasForeignKey(x => x.CourseId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Payment Method Relation
            builder.HasOne(x => x.PaymentMethod)
                   .WithMany(p => p.PaymentTransactions)
                   .HasForeignKey(x => x.PaymentMethodId)
                   .OnDelete(DeleteBehavior.Restrict);

           

            // Indexes (recommended)
            builder.HasIndex(x => x.StudentId);
            builder.HasIndex(x => x.CourseId);
            builder.HasIndex(x => x.Status);
        }
    }
    
}
