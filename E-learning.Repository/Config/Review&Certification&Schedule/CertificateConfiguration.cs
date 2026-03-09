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

            // Primary Key
            builder.HasKey(c => c.Id);

            // Properties
            builder.Property(c => c.CertificateCode)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.FileUrl)
                   .HasMaxLength(500);

            builder.Property(c => c.IssuedAt)
                   .IsRequired();

            // Relationships
            builder.HasOne(c => c.Student)
                   .WithMany() // change if Student has ICollection<Certificate>
                   .HasForeignKey(c => c.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Course)
                   .WithMany() // change if Course has ICollection<Certificate>
                   .HasForeignKey(c => c.CourseId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Index (recommended)
            builder.HasIndex(c => c.CertificateCode)
                   .IsUnique();
        }
    }
}