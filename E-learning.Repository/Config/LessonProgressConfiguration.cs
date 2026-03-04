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
   
    public class LessonProgressConfiguration
        : IEntityTypeConfiguration<LessonProgress>
    {
        public void Configure(EntityTypeBuilder<LessonProgress> builder)
        {
            builder.ToTable("LessonProgress");

            builder.HasKey(lp => lp.Id);

            builder.HasIndex(lp => new { lp.EnrollmentId, lp.LessonId })
                   .IsUnique()
                   .HasDatabaseName("UQ_LessonProgress_Enrollment_Lesson");

            // ─── Properties ─────────────────────────
            builder.Property(lp => lp.Status)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .HasDefaultValue(EnrollmentStatus.NotStarted)
                   .IsRequired();

            builder.Property(lp => lp.WatchedSeconds)
                   .HasDefaultValue(0);

            builder.Property(lp => lp.LastAccessedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

       

            // LessonProgress → Enrollment
            builder.HasOne(lp => lp.Enrollment)
                   .WithMany(e => e.LessonProgresses)
                   .HasForeignKey(lp => lp.EnrollmentId)
                   .OnDelete(DeleteBehavior.Cascade);

            //// LessonProgress → Lesson
            //builder.HasOne(lp => lp.Lesson)
            //       .WithMany()
            //       .HasForeignKey(lp => lp.LessonId)
            //       .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
