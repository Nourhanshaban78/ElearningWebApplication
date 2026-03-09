using E_Learning.Core.Entities.Assessments.Quiz;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class QuizAttemptAnswerConfiguration
    : IEntityTypeConfiguration<QuizAttemptAnswer>
{
    public void Configure(EntityTypeBuilder<QuizAttemptAnswer> builder)
    {
        builder.ToTable("QuizAttemptAnswers");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.TextAnswer)
               .HasMaxLength(1000);

        builder.HasOne(a => a.Attempt)
               .WithMany(at => at.Answers)
               .HasForeignKey(a => a.AttemptId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.Question)
               .WithMany()
               .HasForeignKey(a => a.QuestionId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(a => a.SelectedOption)
               .WithMany()
               .HasForeignKey(a => a.SelectedOptionId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.NoAction);
    }
}