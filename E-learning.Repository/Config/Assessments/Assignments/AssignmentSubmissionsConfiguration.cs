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
    public class AssignmentSubmissionsConfiguration : IEntityTypeConfiguration<AssignmentSubmissions>
    {
        public void Configure(EntityTypeBuilder<AssignmentSubmissions> builder)
        {
           
            builder.ToTable("AssignmentSubmissions");

            builder.HasKey(asub => asub.Id);

            
            builder.Property(asub => asub.FileUrl)
                .HasMaxLength(500);

            builder.Property(asub => asub.Notes)
                .HasMaxLength(1000);

            builder.Property(asub => asub.Score)
                .HasPrecision(7, 2);

            builder.Property(asub => asub.TeacherComment)
                .HasMaxLength(1000);

            builder.Property(asub => asub.Status)
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValue(AssignmentStatus.Pending);

            
            builder.HasOne(asub => asub.Assignment)
                .WithMany(a => a.AssignmentSubmissions)
                .HasForeignKey(asub => asub.AssignmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(asub => asub.Student)
                .WithMany(u => u.AssignmentSubmissions)
                .HasForeignKey(asub => asub.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
          
            builder.HasIndex(asub => new { asub.AssignmentId, asub.StudentId })
                .IsUnique()
                .HasDatabaseName("IX_Unique_Student_Assignment");


           
        }
    }
}
