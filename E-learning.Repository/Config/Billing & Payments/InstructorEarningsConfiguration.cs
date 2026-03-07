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

           
            builder.HasKey(x => x.Id);

          
            builder.Property(x => x.GrossAmount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(x => x.PlatformFee)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(x => x.NetAmount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(x => x.Status)
                   .IsRequired();

            builder.Property(x => x.AvailableAt)
                   .IsRequired();

            builder.Property(x => x.CreatedAt)
                   .IsRequired();

           
            builder.HasOne(x => x.Instructor)
                   .WithMany()
                   .HasForeignKey(x => x.InstructorId)
                   .OnDelete(DeleteBehavior.Restrict);

            
            builder.HasOne(x => x.PaymentTransactions)
                   .WithMany()
                   .HasForeignKey(x => x.TransactionId)
                   .OnDelete(DeleteBehavior.Restrict);

           
            builder.HasOne(x => x.Courses)
                   .WithMany()
                   .HasForeignKey(x => x.CourseId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
