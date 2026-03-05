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

            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Course)
                   .WithMany()
                   .HasForeignKey(c => c.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(c => c.SnapshotDate)
                   .IsRequired();

            builder.Property(c => c.TotalStudents)
                   .HasDefaultValue(0)
                   .IsRequired();

            builder.Property(c => c.ActiveStudents)
                   .HasDefaultValue(0)
                   .IsRequired();

            builder.Property(c => c.AverageGrade)
                   .HasColumnType("DECIMAL(5,2)")
                   .IsRequired(false);

            builder.Property(c => c.CompletionRate)
                   .HasColumnType("DECIMAL(5,2)")
                   .IsRequired(false);

            builder.Property(c => c.AvgWeeklyHours)
                   .HasColumnType("DECIMAL(5,2)")
                   .IsRequired(false);

            builder.Property(c => c.NewEnrollments)
                   .HasDefaultValue(0)
                   .IsRequired();

            builder.Property(c => c.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.HasIndex(c => new { c.CourseId, c.SnapshotDate })
                   .IsUnique()
                   .HasDatabaseName("UX_CourseAnalyticsSnapshots_CourseId_SnapshotDate");

        }
    }
}