using E_Learning.Core.Entities.Assessments.Exams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ExamAttemptAnswerConfiguration
    : IEntityTypeConfiguration<ExamAttemptAnswer>
{
    public void Configure(EntityTypeBuilder<ExamAttemptAnswer> builder)
    {
        builder.ToTable("ExamAttemptAnswers");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Score)
               .HasColumnType("decimal(5,2)");

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