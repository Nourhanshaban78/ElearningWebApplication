using E_Learning.Core.Entities.Assessments.Exams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ExamQuestionConfiguration
    : IEntityTypeConfiguration<ExamQuestion>
{
    public void Configure(EntityTypeBuilder<ExamQuestion> builder)
    {
        builder.ToTable("ExamQuestions");
        builder.HasKey(eq => eq.Id);

        builder.Property(eq => eq.Text)
               .HasMaxLength(1000)
               .IsRequired();

        builder.Property(eq => eq.Type)
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(eq => eq.Points)
               .HasColumnType("decimal(5,2)");

        builder.Property(eq => eq.IsAIGenerated)
               .HasDefaultValue(false);

        builder.HasOne(eq => eq.Exam)
               .WithMany(e => e.Questions)
               .HasForeignKey(eq => eq.ExamId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}