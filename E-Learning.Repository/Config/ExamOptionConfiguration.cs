// Configurations/Assessments/ExamOptionConfiguration.cs
using E_Learning.Core.Entities.Assessments.Exams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ExamOptionConfiguration
    : IEntityTypeConfiguration<ExamOption>
{
    public void Configure(EntityTypeBuilder<ExamOption> builder)
    {
        builder.ToTable("ExamOptions");
        builder.HasKey(eo => eo.Id);

        builder.Property(eo => eo.Text)
               .HasMaxLength(500)
               .IsRequired();

        builder.Property(eo => eo.IsCorrect)
               .HasDefaultValue(false);

        builder.HasOne(eo => eo.Question)
               .WithMany(q => q.Options)
               .HasForeignKey(eo => eo.QuestionId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}