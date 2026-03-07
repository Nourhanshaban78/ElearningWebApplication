using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_learning.Core.Entities.Profiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_learning.Repository.Config.Profiles
{
    public class StudentConfiguration: IEntityTypeConfiguration<Student>
    {
       public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");

            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.EngagementRate)
                   .HasColumnType("decimal(5,2)")
                   .HasDefaultValue(0);

            builder.Property(x => x.CreatedAt)
                   .IsRequired();

            builder.Property(x => x.UpdatedAt)
                   .IsRequired();

            // ─── Relationship: Student ↔ ApplicationUser (1:1)
            builder.HasOne(x => x.User)
                   .WithOne(x => x.Student)
                   .HasForeignKey<Student>(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // ─── Enrollments
            builder.HasMany(x => x.Enrollments)
                   .WithOne(x => x.Student)
                   .HasForeignKey(x => x.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            // ─── Course Reviews
            builder.HasMany(x => x.CourseReviews)
                   .WithOne()
                   .HasForeignKey("StudentId")
                   .OnDelete(DeleteBehavior.Cascade);

            // ─── Quiz Attempts
            builder.HasMany(x => x.QuizAttempts)
                   .WithOne(x => x.Student)
                   .HasForeignKey(x => x.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);

            // ─── Exam Attempts
            builder.HasMany(x => x.ExamAttempts)
                   .WithOne(x => x.Student)
                   .HasForeignKey(x => x.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);

            // ─── Certificates
            builder.HasMany(x => x.Certificates)
                   .WithOne()
                   .HasForeignKey("StudentId")
                   .OnDelete(DeleteBehavior.Cascade);

            // ─── Lesson Progress
            builder.HasMany(x => x.LessonProgresses)
                   .WithOne(x => x.Student)
                   .HasForeignKey(x => x.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);

            // ─── Live Session Attendees
            builder.HasMany(x => x.LiveSessionAttendees)
                   .WithOne()
                   .HasForeignKey("StudentId")
                   .OnDelete(DeleteBehavior.Cascade);

            // ─── Assignment Submissions
            builder.HasMany(x => x.AssignmentSubmissions)
                   .WithOne()
                   .HasForeignKey("StudentId")
                   .OnDelete(DeleteBehavior.Cascade);

            // Index
            builder.HasIndex(x => x.UserId).IsUnique();



        }
    }
}
