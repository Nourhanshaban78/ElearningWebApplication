using E_learning.Core.Entities.Academic_Structure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_learning.Repository.Config.Academic_Structure
{
    public class AcademicStructureConfiguration : IEntityTypeConfiguration<Stage>
    {
        public void Configure(EntityTypeBuilder<Stage> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.Property(x => x.OrderIndex)
                .IsRequired();

            builder.HasMany(x => x.Levels)
                .WithOne(x => x.Stage)
                .HasForeignKey(x => x.StageId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    
    }
}
