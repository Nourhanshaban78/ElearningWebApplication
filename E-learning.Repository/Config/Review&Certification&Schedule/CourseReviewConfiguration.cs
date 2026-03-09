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
            builder.HasKey(r => r.Id);

            // Properties
            builder.Property(r => r.Rating)
                   .IsRequired();

            builder.Property(r => r.Comment)
                   .HasMaxLength(1000);

            builder.Property(r => r.InstructorReply)
                   .HasMaxLength(1000);

            builder.Property(r => r.CreatedAt)
                   .IsRequired();

            // Relationships
            builder.HasOne(r => r.Course)
                   .WithMany() // change if Course has ICollection<CourseReview>
                   .HasForeignKey(r => r.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.Student)
                   .WithMany() // change if Student has ICollection<CourseReview>
                   .HasForeignKey(r => r.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Prevent duplicate review from same student for same course
            builder.HasIndex(r => new { r.CourseId, r.StudentId })
                   .IsUnique();

            // Optional: index for faster rating queries
            builder.HasIndex(r => r.CourseId);
        }
    }
}