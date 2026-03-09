using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_learning.Core.Entities.Assessments.Assignments;
using E_learning.Core.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_learning.Repository.Config.Assessments.Assignments
{
    public class AssignmentSubmissionsConfiguration : IEntityTypeConfiguration<AssignmentSubmission>
    {
        public void Configure(EntityTypeBuilder<AssignmentSubmission> builder)
        {

            builder.ToTable("AssignmentSubmissions");

            // Primary Key
            builder.HasKey(s => s.Id);

            // Properties
            builder.Property(s => s.FileUrl)
                   .HasMaxLength(1000);

            builder.Property(s => s.Notes)
                   .HasMaxLength(2000);

            builder.Property(s => s.TeacherComment)
                   .HasMaxLength(2000);

            builder.Property(s => s.Score)
                   .HasColumnType("decimal(6,2)");

            builder.Property(s => s.Status)
                   .HasDefaultValue(AssignmentStatus.Pending);

            // Relationship: Submission -> Assignment
            builder.HasOne(s => s.Assignment)
                   .WithMany(a => a.AssignmentSubmissions)
                   .HasForeignKey(s => s.AssignmentId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Submission -> Student
            builder.HasOne(s => s.Student)
                   .WithMany(st => st.AssignmentSubmissions)
                   .HasForeignKey(s => s.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(s => s.AssignmentId);
            builder.HasIndex(s => s.StudentId);

            // Prevent student submitting same assignment multiple times
            builder.HasIndex(s => new { s.AssignmentId, s.StudentId })
                   .IsUnique();


        }
    }
}
