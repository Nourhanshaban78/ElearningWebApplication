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
    public class QuizAttemptAnswersConfiguration : IEntityTypeConfiguration<QuizAttemptAnswers>
    {
        public void Configure(EntityTypeBuilder<QuizAttemptAnswers> builder)
        {
            builder.ToTable("QuizAttemptAnswers");

            builder.HasKey(a => a.Id);

             

            builder.Property(a => a.TextAnswer)
                   .HasMaxLength(1000);

            //builder.HasOne(a => a.QuizAttempts)
            //       .WithMany(a => a.QuizAttemptAnswers)
            //       .HasForeignKey(a => a.AttemptId)
            //       .OnDelete(DeleteBehavior.Cascade);

            //builder.HasOne(a => a.QuizQuestions)
            //       .WithMany()
            //       .HasForeignKey(a => a.QuestionId)
            //       .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.QuizOptions)
                   .WithMany(qo=>qo.QuizAttemptAnswers)
                   .HasForeignKey(a => a.SelectedOption)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
