using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_learning.Core.Entities.Profiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_learning.Repository.Config
{
    public class StudentConfiguration: IEntityTypeConfiguration<Student>
    {
       public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.EngagementRate)
                   .HasColumnType("decimal(5,2)")
                   .HasDefaultValue(0);
            builder.Property(s => s.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");
            builder.HasOne(s => s.User)
                   .WithOne(u => u.Student)
                   .HasForeignKey<Student>(s => s.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
                builder.HasMany(s => s.Enrollments)
                        .WithOne(e => e.Student)
                        .HasForeignKey(e => e.StudentId)
                        .OnDelete(DeleteBehavior.Cascade);
                builder.HasMany(s => s.CourseReviews)
                        .WithOne(cr => cr.Student)
                        .HasForeignKey(cr => cr.StudentId)
                        .OnDelete(DeleteBehavior.Cascade);
                builder.HasMany(s => s.QuizAttempts)
                        .WithOne(qa => qa.Student)
                        .HasForeignKey(qa => qa.StudentId)
                        .OnDelete(DeleteBehavior.Cascade);
                builder.HasMany(s => s.ExamAttempts)
                        .WithOne(ea => ea.Student)
                        .HasForeignKey(ea => ea.StudentId)
                        .OnDelete(DeleteBehavior.Cascade);
                builder.HasMany(s => s.PaymentTransactions)
                        .WithOne(pt => pt.Student)
                        .HasForeignKey(pt => pt.StudentId)
                        .OnDelete(DeleteBehavior.Cascade);
                builder.HasMany(s => s.Certificates)
                        .WithOne(c => c.Student)
                        .HasForeignKey(c => c.StudentId)
                        .OnDelete(DeleteBehavior.Cascade);
                builder.HasMany(s => s.LessonProgresses)
                        .WithOne(lp => lp.Student)
                        .HasForeignKey(lp => lp.StudentId)
                        .OnDelete(DeleteBehavior.Cascade);
            


        }
    }
}
