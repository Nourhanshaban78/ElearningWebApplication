using E_learning.Core.Entities.Review_Certification_Schedule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_learning.Repository.Config
{
    public class CertificateConfiguration : IEntityTypeConfiguration<Certificate>
    {
        public void Configure(EntityTypeBuilder<Certificate> builder)
        {
            builder.ToTable("Certificates");

            builder.HasKey(c => c.Id);

            builder.HasIndex(c => new { c.StudentId, c.CourseId })
                   .IsUnique()
                   .HasDatabaseName("UQ_Certificates_Student_Course");

            builder.HasIndex(c => c.CertificateCode)
                   .IsUnique()
                   .HasDatabaseName("UQ_Certificates_Code");

            builder.Property(c => c.CertificateCode)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(c => c.FileUrl)
                   .HasMaxLength(500);

            builder.Property(c => c.IssuedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Relationships

            builder.HasOne(c => c.Student)
                   .WithMany()
                   .HasForeignKey(c => c.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Course)
                   .WithMany()
                   .HasForeignKey(c => c.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}