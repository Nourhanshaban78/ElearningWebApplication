using E_Learning.Core.Entities.Assessments.Quiz;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class QuizConfiguration
    : IEntityTypeConfiguration<Quiz>
{
    public void Configure(EntityTypeBuilder<Quiz> builder)
    {
        builder.ToTable("Quizzes");
        builder.HasKey(q => q.Id);

        builder.Property(q => q.Title)
               .HasMaxLength(200)
               .IsRequired();

        builder.Property(q => q.Type)
               .HasMaxLength(20)
               .HasDefaultValue("Regular");

        builder.Property(q => q.PassingScore)
               .HasColumnType("decimal(5,2)")
               .HasDefaultValue(60);

        builder.Property(q => q.TimePerQuestionSeconds)
               .HasDefaultValue(30);

        builder.Property(q => q.MaxAttempts)
               .HasDefaultValue(3);

        builder.Property(q => q.ShuffleQuestions)
               .HasDefaultValue(true);

        builder.Property(q => q.ShowResultsImmediately)
               .HasDefaultValue(true);

        builder.Property(q => q.IsActive)
               .HasDefaultValue(true);

        builder.HasOne(q => q.Course)
               .WithMany()
               .HasForeignKey(q => q.CourseId)
              .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(q => q.Lesson)
               .WithMany()
               .HasForeignKey(q => q.LessonId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.SetNull);
    }
}