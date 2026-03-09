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
    public class InstructorEarningsConfiguration : IEntityTypeConfiguration<InstructorEarning>
    {
        public void Configure(EntityTypeBuilder<InstructorEarning> builder)
        {
            builder.ToTable("InstructorEarnings");

            // Primary Key
            builder.HasKey(e => e.Id);

            // Properties
            builder.Property(e => e.GrossAmount)
                   .HasColumnType("decimal(10,2)")
                   .IsRequired();

            builder.Property(e => e.PlatformFee)
                   .HasColumnType("decimal(10,2)")
                   .IsRequired();

            builder.Property(e => e.NetAmount)
                   .HasColumnType("decimal(10,2)")
                   .IsRequired();

            builder.Property(e => e.Status)
                   .HasDefaultValue(InstructorEarningsStatus.Pending);

            builder.Property(e => e.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.AvailableAt)
                   .IsRequired();

            // Relationship: Earning -> Instructor
            builder.HasOne(e => e.Instructor)
                   .WithMany(i => i.InstructorEarnings)
                   .HasForeignKey(e => e.InstructorId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasMany(x => x.PaymentTransactions)
               .WithOne(x => x.InstructorEarning)
               .HasForeignKey(x => x.InstructorEarningId)
               .OnDelete(DeleteBehavior.Cascade);



            // Relationship: Earning -> Course
            builder.HasOne(e => e.Course)
                   .WithMany(c => c.InstructorEarnings)
                   .HasForeignKey(e => e.CourseId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(e => e.InstructorId);
            builder.HasIndex(e => e.CourseId);
            builder.HasIndex(e => e.Status);
        }
    }
}
