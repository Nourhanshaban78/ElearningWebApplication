using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_learning.Core.Entities.Assessments.Exams;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_learning.Repository.Config.Assessments.Exam
{
    public class ExamQuestionConfiguration : IEntityTypeConfiguration<ExamQuestions>
    {
        public void Configure(EntityTypeBuilder<ExamQuestions> builder)
        {
            builder.ToTable("ExamQuestions");

            builder.HasKey(q => q.Id);

            builder.Property(q => q.Text)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(q => q.Type)
                     .IsRequired()
                   .HasMaxLength(20);

            builder.Property(q => q.Points)
                   .HasColumnType("decimal(5,2)");
            builder.Property(q => q.IsAIGenerated)
                .HasDefaultValue(false);

            builder.HasOne(q => q.Exams)
                   .WithMany(e => e.ExamQuestions)
                   .HasForeignKey(q => q.ExamId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.Property(q => q.OrderIndex)
                .IsRequired();

        }
    }
}
