using E_Learning.Core.Entities.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class LessonConfiguration
    : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.ToTable("Lessons");
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Title)
               .HasMaxLength(200)
               .IsRequired();

        builder.Property(l => l.Type)
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(l => l.VideoUrl)
               .HasMaxLength(500);

        builder.Property(l => l.IsFreePreview)
               .HasDefaultValue(false);

        builder.Property(l => l.CreatedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(l => l.Section)
               .WithMany(s => s.Lessons)
               .HasForeignKey(l => l.SectionId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}