using E_learning.Core.Entities.Courses___content;
using E_learning.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Repository.Config.Courses___content
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(x => x.Description)
                   .HasMaxLength(2000)
                   .IsRequired();

            builder.Property(x => x.WhatYouWillLearn)
                   .HasMaxLength(4000);

            builder.Property(x => x.ThumbnailUrl)
                   .HasMaxLength(500);

            builder.Property(x => x.Language)
                   .HasMaxLength(10)
                   .HasDefaultValue("en");

            builder.Property(x => x.Price)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(x => x.Status)
                   .IsRequired()
                   .HasDefaultValue(CoursesStatus.Draft);

            builder.Property(x => x.IsActive)
                   .HasDefaultValue(true);

            builder.Property(x => x.CreatedAt)
                   .IsRequired();

            builder.Property(x => x.UpdatedAt)
                   .IsRequired();

            builder.Property(x => x.ApprovedAt);

            // Instructor Relation
            builder.HasOne(x => x.Instructor)
                   .WithMany(i => i.Courses)
                   .HasForeignKey(x => x.InstructorId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Level Relation (Optional)
            builder.HasOne(x => x.Level)
                   .WithMany(l => l.Courses)
                   .HasForeignKey(x => x.LevelId)
                   .OnDelete(DeleteBehavior.SetNull);

            // Admin Approval Relation
            builder.HasOne(x => x.ApprovedBy)
                   .WithMany()
                   .HasForeignKey(x => x.ApprovedById)
                   .OnDelete(DeleteBehavior.SetNull);

            // Sections
            builder.HasMany(x => x.Sections)
                   .WithOne(s => s.Course)
                   .HasForeignKey(s => s.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Quizzes
            builder.HasMany(x => x.Quizzes)
                   .WithOne(q => q.Course)
                   .HasForeignKey(q => q.CourseId)
                   .OnDelete(DeleteBehavior.NoAction);

            // Exams
            builder.HasMany(x => x.Exams)
                   .WithOne(e => e.Course)
                   .HasForeignKey(e => e.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Payment Transactions
            builder.HasMany(x => x.PaymentTransactions)
                   .WithOne(p => p.Course)
                   .HasForeignKey(p => p.CourseId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Instructor Earnings
            builder.HasMany(x => x.InstructorEarnings)
                   .WithOne(e => e.Course)
                   .HasForeignKey(e => e.CourseId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Reviews
            builder.HasMany(x => x.CourseReviews)
                   .WithOne(r => r.Course)
                   .HasForeignKey(r => r.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Enrollments
            builder.HasMany(x => x.Enrollments)
                   .WithOne(e => e.Course)
                   .HasForeignKey(e => e.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Assignments
            builder.HasMany(x => x.Assignments)
                   .WithOne(a => a.Course)
                   .HasForeignKey(a => a.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Certificates
            builder.HasMany(x => x.Certificates)
                   .WithOne(c => c.Course)
                   .HasForeignKey(c => c.CourseId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Analytics Snapshots
            builder.HasMany(x => x.AnalyticsSnapshots)
                   .WithOne(a => a.Course)
                   .HasForeignKey(a => a.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Indexes (recommended)
            builder.HasIndex(x => x.InstructorId);
            builder.HasIndex(x => x.LevelId);
            builder.HasIndex(x => x.Status);
            builder.HasIndex(x => x.IsActive);
        }
    }
}
