using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_learning.Core.Entities.Assessments.Assignments;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_learning.Repository.Config.Assessments.Assignments
{
    public class AssignmentsConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {

            builder.ToTable("Assignments");

            // Primary Key
            builder.HasKey(a => a.Id);

            // Properties
            builder.Property(a => a.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(a => a.Description)
                   .HasMaxLength(2000);

            builder.Property(a => a.TotalMarks)
                   .HasColumnType("decimal(6,2)")
                   .IsRequired();

            builder.Property(a => a.DueDate)
                   .IsRequired();

            builder.Property(a => a.IsActive)
                   .HasDefaultValue(true);

            builder.Property(a => a.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Relationship: Assignment -> Course
            builder.HasOne(a => a.Course)
                   .WithMany(c => c.Assignments)
                   .HasForeignKey(a => a.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Assignment -> Submissions
            builder.HasMany(a => a.AssignmentSubmissions)
                   .WithOne(s => s.Assignment)
                   .HasForeignKey(s => s.AssignmentId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Index
            builder.HasIndex(a => a.CourseId);

        }
    }
}
