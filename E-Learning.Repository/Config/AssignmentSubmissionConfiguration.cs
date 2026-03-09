using E_Learning.Core.Entities.Assessments.Assignments;
using E_Learning.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AssignmentSubmissionConfiguration
    : IEntityTypeConfiguration<AssignmentSubmission>
{
    public void Configure(EntityTypeBuilder<AssignmentSubmission> builder)
    {
        builder.ToTable("AssignmentSubmissions");
        builder.HasKey(s => s.Id);

        builder.HasIndex(s => new { s.AssignmentId, s.StudentId })
               .IsUnique()
               .HasDatabaseName("UQ_Submission_Assignment_Student");

        builder.Property(s => s.FileUrl)
               .HasMaxLength(500);

        builder.Property(s => s.Notes)
               .HasMaxLength(1000);

        builder.Property(s => s.Score)
               .HasColumnType("decimal(7,2)");

        builder.Property(s => s.Status)
               .HasConversion<string>()
               .HasMaxLength(20)
               .HasDefaultValue(AssignmentStatus.Pending);

        builder.HasOne(s => s.Assignment)
               .WithMany(a => a.Submissions)
               .HasForeignKey(s => s.AssignmentId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.Student)
               .WithMany()
               .HasForeignKey(s => s.StudentId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}