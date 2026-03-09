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

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EngagementRate)
                   .HasColumnType("decimal(5,2)")
                   .HasDefaultValue(0);

            builder.Property(x => x.CreatedAt)
                   .IsRequired();

            builder.Property(x => x.UpdatedAt)
                   .IsRequired();

            // User relation (1-1)
            builder.HasOne(x => x.User)
                   .WithOne(u => u.Student)
                   .HasForeignKey<Student>(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Instructor relation
            builder.HasOne(x => x.Instructor)
                   .WithMany(i => i.Students)
                   .HasForeignKey(x => x.InstructorId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Enrollments
            builder.HasMany(x => x.Enrollments)
                   .WithOne(e => e.Student)
                   .HasForeignKey(e => e.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Course Reviews
            builder.HasMany(x => x.CourseReviews)
                   .WithOne(r => r.Student)
                   .HasForeignKey(r => r.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Quiz Attempts
            builder.HasMany(x => x.QuizAttempts)
                   .WithOne(q => q.Student)
                   .HasForeignKey(q => q.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Exam Attempts
            builder.HasMany(x => x.ExamAttempts)
                   .WithOne(e => e.Student)
                   .HasForeignKey(e => e.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Certificates
            builder.HasMany(x => x.Certificates)
                   .WithOne(c => c.Student)
                   .HasForeignKey(c => c.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Lesson Progress
            builder.HasMany(x => x.LessonProgresses)
                   .WithOne(lp => lp.Student)
                   .HasForeignKey(lp => lp.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Live Session Attendees
            builder.HasMany(x => x.LiveSessionAttendees)
                   .WithOne(a => a.Student)
                   .HasForeignKey(a => a.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Assignment Submissions
            builder.HasMany(x => x.AssignmentSubmissions)
                   .WithOne(a => a.Student)
                   .HasForeignKey(a => a.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Payment Transactions
            builder.HasMany(x => x.PaymentTransactions)
                   .WithOne(p => p.Student)
                   .HasForeignKey(p => p.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(x => x.UserId).IsUnique();
            builder.HasIndex(x => x.InstructorId);


        }
    }
}
