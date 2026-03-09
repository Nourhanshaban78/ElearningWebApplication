using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_learning.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_learning.Repository.Config.AdminOperationsConfiguration
{
    public class CourseAnalyticsSnapshotsConfig : IEntityTypeConfiguration<CourseAnalyticsSnapshots>
    {
        public void Configure(EntityTypeBuilder<CourseAnalyticsSnapshots> builder)
        {
            builder.ToTable("CourseAnalyticsSnapshots");

            // Primary Key
            builder.HasKey(c => c.Id);

            // Properties
            builder.Property(c => c.SnapshotDate)
                   .IsRequired();

            builder.Property(c => c.TotalStudents)
                   .HasDefaultValue(0);

            builder.Property(c => c.ActiveStudents)
                   .HasDefaultValue(0);

            builder.Property(c => c.NewEnrollments)
                   .HasDefaultValue(0);

            builder.Property(c => c.AverageGrade)
                   .HasColumnType("decimal(5,2)");

            builder.Property(c => c.CompletionRate)
                   .HasColumnType("decimal(5,2)");

            builder.Property(c => c.AvgWeeklyHours)
                   .HasColumnType("decimal(5,2)");

            builder.Property(c => c.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Relationship with Course
            builder.HasOne(c => c.Course)
                   .WithMany(c => c.AnalyticsSnapshots)
                   .HasForeignKey(c => c.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Indexes (recommended for analytics queries)
            builder.HasIndex(c => c.CourseId);

            builder.HasIndex(c => new { c.CourseId, c.SnapshotDate })
                   .IsUnique();
        }
    }
}