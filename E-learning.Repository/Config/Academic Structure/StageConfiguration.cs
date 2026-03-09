using E_learning.Core.Entities.Academic_Structure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_learning.Repository.Config.Academic_Structure
{
    public class AcademicStructureConfiguration : IEntityTypeConfiguration<Stage>
    {
        public void Configure(EntityTypeBuilder<Stage> builder)
        {
            builder.ToTable("Stages");

            // Primary Key
            builder.HasKey(s => s.Id);

            // Properties
            builder.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(s => s.Description)
                   .HasMaxLength(500);

            builder.Property(s => s.OrderIndex)
                   .IsRequired();

            builder.Property(s => s.IsActive)
                   .HasDefaultValue(true);

            builder.Property(s => s.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Relationship with Levels
            builder.HasMany(s => s.Levels)
                   .WithOne(l => l.Stage)
                   .HasForeignKey(l => l.StageId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Index (recommended)
            builder.HasIndex(s => s.OrderIndex)
                   .IsUnique();
        }

    }
}
