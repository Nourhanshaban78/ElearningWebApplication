using E_learning.Core.Entities;
using E_learning.Core.Entities.Enrollment___Progress;
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

    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.ToTable("Enrollments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                   .IsRequired()
                   .HasDefaultValue(EnrollmentStatus.NotStarted);

            builder.Property(x => x.ProgressPercentage)
                   .HasColumnType("decimal(5,2)")
                   .HasDefaultValue(0);

            builder.Property(x => x.CompletedAt);

            builder.Property(x => x.IsDeleted)
                   .HasDefaultValue(false);

            builder.Property(x => x.DeletedBy)
                   .HasMaxLength(200);

            // Student Relation
            builder.HasOne(x => x.Student)
                   .WithMany(s => s.Enrollments)
                   .HasForeignKey(x => x.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Course Relation
            builder.HasOne(x => x.Course)
                   .WithMany(c => c.Enrollments)
                   .HasForeignKey(x => x.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Payment Transaction Relation
            builder.HasOne(x => x.Transaction)
                   .WithMany()
                   .HasForeignKey(x => x.TransactionId)
                   .OnDelete(DeleteBehavior.SetNull);

            // Lesson Progress Relation
            builder.HasMany(x => x.LessonProgresses)
                   .WithOne(lp => lp.Enrollment)
                   .HasForeignKey(lp => lp.EnrollmentId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(x => x.StudentId);
            builder.HasIndex(x => x.CourseId);

            // Prevent duplicate enrollment for same student and course
            builder.HasIndex(x => new { x.StudentId, x.CourseId })
                   .IsUnique();
        }
    
}
}
