using E_Learning.Core.Entities.Assessments.Exams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ExamConfiguration
    : IEntityTypeConfiguration<Exam>
{
    public void Configure(EntityTypeBuilder<Exam> builder)
    {
        builder.ToTable("Exams");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
               .HasMaxLength(200)
               .IsRequired();

        builder.Property(e => e.TotalMarks)
               .HasColumnType("decimal(7,2)");

        builder.Property(e => e.PassingScore)
               .HasColumnType("decimal(5,2)")
               .HasDefaultValue(60);

        builder.Property(e => e.MaxAttempts)
               .HasDefaultValue(1);

        builder.Property(e => e.AIShuffleEnabled)
               .HasDefaultValue(true);

        builder.Property(e => e.IsActive)
               .HasDefaultValue(true);

        builder.HasOne(e => e.Course)
               .WithMany()
               .HasForeignKey(e => e.CourseId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}