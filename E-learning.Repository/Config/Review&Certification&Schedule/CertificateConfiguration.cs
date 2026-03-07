using E_learning.Core.Entities.Review_Certification_Schedule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_learning.Repository.Config
{
    public class CertificateConfiguration : IEntityTypeConfiguration<Certificate>
    {
        public void Configure(EntityTypeBuilder<Certificate> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CertificateCode)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.FileUrl)
                .HasMaxLength(500);

            // Student relation
            builder.HasOne(x => x.Student)
                .WithMany(x => x.Certificates)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Course relation
            builder.HasOne(x => x.Course)
                .WithMany(x => x.Certificates)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.CertificateCode)
                .IsUnique();
        }
    }
}