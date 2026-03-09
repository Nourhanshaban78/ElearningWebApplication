using E_Learning.Core.Entities.Reviews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CertificateConfiguration
    : IEntityTypeConfiguration<Certificate>
{
    public void Configure(EntityTypeBuilder<Certificate> builder)
    {
        builder.ToTable("Certificates");
        builder.HasKey(c => c.Id);

        builder.HasIndex(c => new { c.StudentId, c.CourseId })
               .IsUnique()
               .HasDatabaseName("UQ_Certificate_Student_Course");

        builder.HasIndex(c => c.CertificateCode)
               .IsUnique()
               .HasDatabaseName("UQ_Certificate_Code");

        builder.Property(c => c.CertificateCode)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(c => c.FileUrl)
               .HasMaxLength(500);

        builder.Property(c => c.IssuedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(c => c.Student)
               .WithMany()
               .HasForeignKey(c => c.StudentId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Course)
               .WithMany()
               .HasForeignKey(c => c.CourseId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}