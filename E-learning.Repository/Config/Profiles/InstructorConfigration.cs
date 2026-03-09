using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using E_learning.Core.Entities.Profiles;
using E_learning.Core.Entities.Identity;

using System.Threading.Tasks;

namespace E_learning.Repository.Config.Profiles
{
    public class InstructorConfigration : IEntityTypeConfiguration<Instructor>

    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.ToTable("Instructors");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Headline)
                   .HasMaxLength(200);

            builder.Property(x => x.About)
                   .HasMaxLength(2000);

            builder.Property(x => x.TotalEarnings)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);

            builder.Property(x => x.PendingPayout)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);

            builder.Property(x => x.CreatedAt)
                   .IsRequired();

            builder.Property(x => x.UpdatedAt)
                   .IsRequired();

            // User Relation (One-to-One)
            builder.HasOne(x => x.User)
                   .WithOne(u => u.Instructor)
                   .HasForeignKey<Instructor>(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Courses
            builder.HasMany(x => x.Courses)
                   .WithOne(c => c.Instructor)
                   .HasForeignKey(c => c.InstructorId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Students
            builder.HasMany(x => x.Students)
                   .WithOne(s => s.Instructor)
                   .HasForeignKey(s => s.InstructorId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Exams
            builder.HasMany(x => x.Exams)
                   .WithOne(e => e.Instructor)
                   .HasForeignKey(e => e.InstructorId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Quizzes
            builder.HasMany(x => x.Quizzes)
                   .WithOne(q => q.Instructor)
                   .HasForeignKey(q => q.InstructorId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Instructor Earnings
            builder.HasMany(x => x.InstructorEarnings)
                   .WithOne(e => e.Instructor)
                   .HasForeignKey(e => e.InstructorId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Live Sessions
            builder.HasMany(x => x.LiveSessions)
                   .WithOne(ls => ls.Instructor)
                   .HasForeignKey(ls => ls.InstructorId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Payout Requests
            builder.HasMany(x => x.PayoutRequests)
                   .WithOne(p => p.Instructor)
                   .HasForeignKey(p => p.InstructorId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Schedule Events
            builder.HasMany(x => x.ScheduleEvents)
                   .WithOne(se => se.Instructor)
                   .HasForeignKey(se => se.InstructorId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Index
            builder.HasIndex(x => x.UserId).IsUnique();
        }
    }
}
