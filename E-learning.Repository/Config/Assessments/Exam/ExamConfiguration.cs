using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using E_learning.Core.Entities.Assessments.Exams;

namespace E_learning.Repository.Config.Assessments.Exam
{
    public class ExamConfiguration : IEntityTypeConfiguration<Exams>
    {
        public void Configure(EntityTypeBuilder<Exams> builder)
        {
            builder.ToTable("Exams");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(e => e.TotalMarks)
                   .IsRequired()
                   .HasColumnType("decimal(7,2)");

            builder.Property(e => e.PassingScore)
                   .HasColumnType("decimal(5,2)")
                   .HasDefaultValue(60);

            builder.Property(e => e.MaxAttempts)
                   .HasDefaultValue(1);

            builder.Property(e => e.AIShuffleEnabled)
                   .HasDefaultValue(true);

            builder.Property(e => e.IsActive)
                   .HasDefaultValue(true);

            builder.HasOne(e => e.Courses)
                   .WithMany(c => c.Exams)
                   .HasForeignKey(e => e.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.Property(e => e.ScheduledAt)
                .IsRequired();
            builder.Property(e => e.SourceFileUrl)
               .HasMaxLength(500);
            builder.Property(e => e.EducationLevel)
            .HasMaxLength(50);

        }
    }
}
