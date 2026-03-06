using E_learning.Core.Entities.Review_Certification_Schedule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_learning.Repository.Config
{
    public class CourseReviewConfiguration : IEntityTypeConfiguration<CourseReview>
    {
        public void Configure(EntityTypeBuilder<CourseReview> builder)
        {
            builder.ToTable("CourseReviews");

            builder.HasKey(cr => cr.Id);

            builder.HasIndex(cr => new { cr.CourseId, cr.StudentId })
                   .IsUnique()
                   .HasDatabaseName("UQ_CourseReviews_Course_Student");

            builder.Property(cr => cr.Rating)
                   .IsRequired();

            builder.Property(cr => cr.Comment)
                   .HasMaxLength(1000);

            builder.Property(cr => cr.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.ToTable(t =>
            {
                t.HasCheckConstraint("CK_CourseReviews_Rating", "[Rating] BETWEEN 1 AND 5");
            });
            // Relationships

            builder.HasOne(cr => cr.Course)
                   .WithMany(c => c.CourseReviews)  // need in Course entity : 
                   .HasForeignKey(cr => cr.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cr => cr.Student)
                   .WithMany()
                   .HasForeignKey(cr => cr.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}