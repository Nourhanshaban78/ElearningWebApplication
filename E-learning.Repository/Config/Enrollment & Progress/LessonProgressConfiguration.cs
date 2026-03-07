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
                   .IsRequired();

            builder.Property(x => x.WatchedSeconds)
                   .HasDefaultValue(0);

            builder.Property(x => x.LastAccessedAt)
                   .IsRequired();

          
            builder.HasOne(x => x.Enrollment)
                   .WithMany(x => x.LessonProgresses)
                   .HasForeignKey(x => x.EnrollmentId)
                   .OnDelete(DeleteBehavior.Cascade);

          
            builder.HasOne(x => x.Lesson)
                   .WithMany()
                   .HasForeignKey(x => x.LessonId)
                   .OnDelete(DeleteBehavior.Cascade);

           
            builder.HasOne(x => x.Student)
                   .WithMany()
                   .HasForeignKey(x => x.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.StudentId, x.LessonId })
                   .IsUnique();

           
            builder.HasIndex(x => x.EnrollmentId);
            builder.HasIndex(x => x.LessonId);
        }
    }
}
