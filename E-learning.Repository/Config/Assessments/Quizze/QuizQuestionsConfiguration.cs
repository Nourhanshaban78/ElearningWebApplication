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
    public class QuizQuestionsConfiguration : IEntityTypeConfiguration<QuizQuestions>
    {
        public void Configure(EntityTypeBuilder<QuizQuestions> builder)
        {
            builder.ToTable("QuizQuestions");

            builder.HasKey(q => q.Id);

            builder.Property(q => q.Text)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(q => q.Type)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(q => q.Points)
                   .HasColumnType("decimal(5,2)")
                   .HasDefaultValue(1);

            builder.Property(q => q.IsAIGenerated)
                   .HasDefaultValue(false);

            builder.Property(q => q.OrderIndex)
                   .IsRequired();

            builder.HasOne(q => q.Quizzes)
                   .WithMany(q => q.QuizQuestions)
                   .HasForeignKey(q => q.QuizId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
