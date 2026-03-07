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
            builder.ToTable("PaymentTransactions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(x => x.Currency)
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(x => x.Status)
                   .IsRequired();

            builder.Property(x => x.GatewayReference)
                   .HasMaxLength(200);

            builder.Property(x => x.FailureReason)
                   .HasMaxLength(500);

            builder.Property(x => x.CreatedAt)
                   .IsRequired();

            builder.HasOne(x => x.Student)
                   .WithMany()
                   .HasForeignKey(x => x.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Courses)
                   .WithMany()
                   .HasForeignKey(x => x.CourseId)
                   .OnDelete(DeleteBehavior.Restrict);

                    builder.HasOne(t => t.PaymentMethods)
                .WithMany(p => p.PaymentTransactions)
               .HasForeignKey(t => t.PaymentMethodId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.InstructorEarnings)
                   .WithOne(x => x.PaymentTransactions)
                   .HasForeignKey(x => x.TransactionId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.StudentId);
            builder.HasIndex(x => x.CourseId);
            builder.HasIndex(x => x.Status);
        }
    }
}
