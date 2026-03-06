using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Repository.Config.Identity
{
    using E_learning.Core.Entities.Identity;
    using E_learning.Core.Entities.Notifactions;
    using E_learning.Core.Entities.Profiles;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("Users");

            builder.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(u => u.Bio)
                .HasMaxLength(1000);

            builder.Property(u => u.ProfileImage)
                .HasMaxLength(500);

            builder.Property(u => u.Location)
                .HasMaxLength(200);

            builder.Property(u => u.Language)
                .HasMaxLength(10)
                .HasDefaultValue("en");

            builder.Property(u => u.TimeZone)
                .HasMaxLength(50)
                .HasDefaultValue("UTC");

            builder.Property(u => u.MemberSince)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(u => u.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(u => u.IsActive)
                .HasConversion<int>();

         
            // One To One (NotificationSettings)
         
            builder.HasOne(u => u.NotificationSettings)
                .WithOne(n => n.User)
                .HasForeignKey<NotificationSettings>(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

         
            // One To Many (Notifications)
         
            builder.HasMany(u => u.Notifications)
                .WithOne(n => n.User)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

         
            // Courses Created by Instructor
         
            builder.HasMany(u => u.Courses)
                .WithOne(c => c.Instructor)
                .HasForeignKey(c => c.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

         
            // Courses Approved by Admin
         
            builder.HasMany(u => u.ApprovedCourses)
                .WithOne(c => c.ApprovedBy)
                .HasForeignKey(c => c.ApprovedById)
                .OnDelete(DeleteBehavior.Restrict);

         
            // ExamAttempts
         
            builder.HasMany(u => u.ExamAttempts)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId);

         
            // QuizAttempts
         
            builder.HasMany(u => u.QuizAttempts)
                .WithOne(q => q.Students)
                .HasForeignKey(q => q.StudentId);

         
            // Enrollments
         
            builder.HasMany(u => u.Enrollments)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId);

         
            // Instructor Earnings
         
            builder.HasMany(u => u.InstructorEarnings)
                .WithOne(i => i.Instructor)
                .HasForeignKey(i => i.InstructorId);

         
            // Payment Methods
         
            builder.HasMany(u => u.PaymentMethods)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

         
            // Payment Transactions
         
            builder.HasMany(u => u.PaymentTransactions)
                .WithOne(p => p.Student)
                .HasForeignKey(p => p.StudentId);

         
            // Payout Requests
         
            builder.HasMany(u => u.PayoutRequests)
                .WithOne(p => p.Instructor)
                .HasForeignKey(p => p.InstructorId);

         
            // User Sessions
         
            builder.HasMany(u => u.UserSessions)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId);

            
            // OTP Codes
         
            builder.HasMany(u => u.OtpCodes)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);

            //Profile
            builder.HasOne(u => u.Student)
                .WithOne(s => s.User)
                .HasForeignKey<Student>(s => s.UserId);

            builder.HasOne(u => u.Instructor)
                .WithOne(i => i.User)
                .HasForeignKey<Instructor>(i => i.UserId);

            builder.HasOne(u => u.Admin)
                .WithOne(a => a.User)
                .HasForeignKey<Admin>(a => a.UserId);
        }
    }
}
