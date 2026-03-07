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

            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Rating)
                   .IsRequired();

            builder.Property(x => x.Comment)
                   .HasMaxLength(2000);

            builder.Property(x => x.InstructorReply)
                   .HasMaxLength(2000);

            builder.Property(x => x.CreatedAt)
                   .IsRequired();

            // Relationship: Course
            builder.HasOne(x => x.Course)
                   .WithMany(x => x.CourseReviews)
                   .HasForeignKey(x => x.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Student
            builder.HasOne(x => x.Student)
                   .WithMany(x => x.CourseReviews)
                   .HasForeignKey(x => x.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Prevent duplicate review by same student for the same course
            builder.HasIndex(x => new { x.CourseId, x.StudentId })
                   .IsUnique();

            // Index for performance
            builder.HasIndex(x => x.CourseId);
        }
    }
}