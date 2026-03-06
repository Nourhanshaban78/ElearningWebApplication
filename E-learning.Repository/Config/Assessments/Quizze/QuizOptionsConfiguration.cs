using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_learning.Core.Entities.Assessments.Quizzes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_learning.Repository.Config.Assessments.Quizze
{
    public class QuizOptionsConfiguration : IEntityTypeConfiguration<QuizOptions>
    {
        public void Configure(EntityTypeBuilder<QuizOptions> builder)
        {
            builder.ToTable("QuizOptions");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Text)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(o => o.IsCorrect)
                   .HasDefaultValue(false);

            builder.Property(o => o.OrderIndex)
                   .IsRequired();

            builder.HasOne(o => o.QuizQuestions)
                   .WithMany(q => q.QuizOptions)
                   .HasForeignKey(o => o.QuestionId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
