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
    public class ExamOptionConfiguration : IEntityTypeConfiguration<ExamOptions>
    {
        public void Configure(EntityTypeBuilder<ExamOptions> builder)
        {
            builder.ToTable("ExamOptions");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Text)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(o => o.IsCorrect)
                   .HasDefaultValue(false);
            builder.Property(q => q.OrderIndex)
                  .IsRequired();

            builder.HasOne(o => o.ExamQuestions)
                   .WithMany(q => q.ExamOptions)
                   .HasForeignKey(o => o.QuestionId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
