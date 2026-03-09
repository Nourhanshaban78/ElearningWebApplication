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
   
    public class LessonProgressConfiguration
        : IEntityTypeConfiguration<LessonProgress>
    {
        public void Configure(EntityTypeBuilder<LessonProgress> builder)
        {
            builder.ToTable("LessonProgresses");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                   .IsRequired()
                   .HasDefaultValue(EnrollmentStatus.NotStarted);

            builder.Property(x => x.WatchedSeconds)
                   .IsRequired()
                   .HasDefaultValue(0);

            builder.Property(x => x.LastAccessedAt)
                   .IsRequired();

            builder.Property(x => x.CompletedAt);

            // Enrollment Relation
            builder.HasOne(x => x.Enrollment)
                   .WithMany(e => e.LessonProgresses)
                   .HasForeignKey(x => x.EnrollmentId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Lesson Relation
            builder.HasOne(x => x.Lesson)
                   .WithMany()
                   .HasForeignKey(x => x.LessonId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Student Relation
            builder.HasOne(x => x.Student)
                   .WithMany(s => s.LessonProgresses)
                   .HasForeignKey(x => x.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(x => x.EnrollmentId);
            builder.HasIndex(x => x.LessonId);
            builder.HasIndex(x => x.StudentId);

            // Prevent duplicate progress record for same lesson in same enrollment
            builder.HasIndex(x => new { x.EnrollmentId, x.LessonId })
                   .IsUnique();

        }
    }
}
