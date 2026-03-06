using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_learning.Core.Entities.Profiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_learning.Repository.Config.Profile
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

            builder.Property(s => s.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(s => s.User)
                .WithOne(u => u.Student)
                .HasForeignKey<Student>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(s => s.Enrollments)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId);

            
            builder.HasMany(s => s.CourseReviews)
                .WithOne(r => r.Student)
                .HasForeignKey(r => r.StudentId);

           
            builder.HasMany(s => s.QuizAttempts)
                .WithOne(q => q.Student)
                .HasForeignKey(q => q.StudentId);

            builder.HasMany(s => s.ExamAttempts)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId);

            builder.HasMany(s => s.PaymentTransactions)
                .WithOne(p => p.Student)
                .HasForeignKey(p => p.StudentId);

            builder.HasMany(s => s.Certificates)
                .WithOne(c => c.Student)
                .HasForeignKey(c => c.StudentId);

            builder.HasMany(s => s.LessonProgresses)
                .WithOne(lp => lp.Student)
                .HasForeignKey(lp => lp.StudentId);

            builder.HasMany(s => s.AssignmentSubmissions)
                .WithOne(a => a.Student)
                .HasForeignKey(a => a.StudentId);


            builder.HasIndex(s => s.UserId).IsUnique();
        }

    }
    }
}
