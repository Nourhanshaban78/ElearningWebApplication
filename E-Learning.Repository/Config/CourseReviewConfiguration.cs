using E_Learning.Core.Entities.Reviews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CourseReviewConfiguration
    : IEntityTypeConfiguration<CourseReview>
{
    public void Configure(EntityTypeBuilder<CourseReview> builder)
    {
        builder.ToTable("CourseReviews");
        builder.HasKey(cr => cr.Id);

        builder.HasIndex(cr => new { cr.CourseId, cr.StudentId })
               .IsUnique()
               .HasDatabaseName("UQ_Review_Course_Student");

        builder.Property(cr => cr.Rating)
               .IsRequired();

        builder.ToTable(t => t.HasCheckConstraint(
            "CK_CourseReview_Rating",
            "[Rating] >= 1 AND [Rating] <= 5"));

        builder.Property(cr => cr.Comment)
               .HasMaxLength(1000);

        builder.Property(cr => cr.InstructorReply)
               .HasMaxLength(1000);

        builder.Property(cr => cr.CreatedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(cr => cr.Course)
               .WithMany()
               .HasForeignKey(cr => cr.CourseId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cr => cr.Student)
               .WithMany()
               .HasForeignKey(cr => cr.StudentId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}