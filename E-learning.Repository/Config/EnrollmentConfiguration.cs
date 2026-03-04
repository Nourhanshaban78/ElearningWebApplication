using E_learning.Core.Entities;
using E_learning.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Repository.Config
{
    
    public class EnrollmentConfiguration
        : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.ToTable("Enrollments");

            builder.HasKey(e => e.Id);

            builder.HasIndex(e => new { e.StudentId, e.CourseId })
                   .IsUnique()
                   .HasDatabaseName("UQ_Enrollment_Student_Course");

            builder.Property(e => e.Status)
                   .HasConversion<string>()   
                   .HasMaxLength(20)
                   .HasDefaultValue(EnrollmentStatus.NotStarted)
                   .IsRequired();

            builder.Property(e => e.ProgressPercentage)
                   .HasColumnType("decimal(5,2)")
                   .HasDefaultValue(0);


            //builder.HasOne(e => e.Student)
            //       .WithMany(u => u.Enrollments)
            //       .HasForeignKey(e => e.StudentId)
            //       .OnDelete(DeleteBehavior.Restrict);

            //// Enrollment → Course
            //builder.HasOne(e => e.Course)
            //       .WithMany(c => c.Enrollments)
            //       .HasForeignKey(e => e.CourseId)
            //       .OnDelete(DeleteBehavior.Restrict);

            //// Enrollment → Transaction (Optional)
            //builder.HasOne(e => e.Transaction)
            //       .WithMany()
            //       .HasForeignKey(e => e.TransactionId)
            //       .IsRequired(false)
            //       .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
