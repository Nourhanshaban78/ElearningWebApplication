// Configurations/Courses/SectionConfiguration.cs
using E_Learning.Core.Entities.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SectionConfiguration
    : IEntityTypeConfiguration<Section>
{
    public void Configure(EntityTypeBuilder<Section> builder)
    {
        builder.ToTable("Sections");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Title)
               .HasMaxLength(200)
               .IsRequired();

        builder.Property(s => s.CreatedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(s => s.Course)
               .WithMany()
               .HasForeignKey(s => s.CourseId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}