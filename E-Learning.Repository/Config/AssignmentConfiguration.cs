using E_Learning.Core.Entities.Assessments.Assignments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AssignmentConfiguration
    : IEntityTypeConfiguration<Assignment>
{
    public void Configure(EntityTypeBuilder<Assignment> builder)
    {
        builder.ToTable("Assignments");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Title)
               .HasMaxLength(200)
               .IsRequired();

        builder.Property(a => a.TotalMarks)
               .HasColumnType("decimal(7,2)");

        builder.Property(a => a.IsActive)
               .HasDefaultValue(true);

        builder.Property(a => a.CreatedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(a => a.Course)
               .WithMany()
               .HasForeignKey(a => a.CourseId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}