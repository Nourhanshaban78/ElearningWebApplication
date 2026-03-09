using E_Learning.Core.Entities.Assessments.Quiz;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class QuizQuestionConfiguration
    : IEntityTypeConfiguration<QuizQuestion>
{
    public void Configure(EntityTypeBuilder<QuizQuestion> builder)
    {
        builder.ToTable("QuizQuestions");
        builder.HasKey(qq => qq.Id);

        builder.Property(qq => qq.Text)
               .HasMaxLength(1000)
               .IsRequired();

        builder.Property(qq => qq.Type)
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(qq => qq.Points)
               .HasColumnType("decimal(5,2)")
               .HasDefaultValue(1);

        builder.Property(qq => qq.IsAIGenerated)
               .HasDefaultValue(false);

        builder.HasOne(qq => qq.Quiz)
               .WithMany(q => q.Questions)
               .HasForeignKey(qq => qq.QuizId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}