using E_Learning.Core.Entities.Assessments.Quiz;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class QuizOptionConfiguration
    : IEntityTypeConfiguration<QuizOption>
{
    public void Configure(EntityTypeBuilder<QuizOption> builder)
    {
        builder.ToTable("QuizOptions");
        builder.HasKey(qo => qo.Id);

        builder.Property(qo => qo.Text)
               .HasMaxLength(500)
               .IsRequired();

        builder.Property(qo => qo.IsCorrect)
               .HasDefaultValue(false);

        builder.HasOne(qo => qo.Question)
               .WithMany(q => q.Options)
               .HasForeignKey(qo => qo.QuestionId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}