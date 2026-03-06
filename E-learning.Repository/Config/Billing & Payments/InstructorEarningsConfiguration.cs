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
    public class InstructorEarningsConfiguration : IEntityTypeConfiguration<InstructorEarnings>
    {
        public void Configure(EntityTypeBuilder<InstructorEarnings> builder)
        {
            builder.ToTable("InstructorEarnings");

            builder.HasKey(e => e.Id);
     

            builder.Property(e => e.GrossAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(e => e.PlatformFee)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(e => e.NetAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(e => e.Status)
                .HasConversion<int>()
                .HasDefaultValue(InstructorEarningsStatus.Pending);

            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.AvailableAt)
                .IsRequired();

            
            // Relationships
            

            // Instructor (User)
            builder.HasOne(e => e.Instructor)
                .WithMany(u => u.InstructorEarnings)
                .HasForeignKey(e => e.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Transaction
            builder.HasOne(e => e.PaymentTransactions)
                .WithMany(t => t.InstructorEarnings)
                .HasForeignKey(e => e.TransactionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Course
            builder.HasOne(e => e.Courses)
                .WithMany(c => c.InstructorEarnings)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
