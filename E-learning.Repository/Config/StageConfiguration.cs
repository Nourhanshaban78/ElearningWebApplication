using E_learning.Core.Entities.Academic_Structure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Repository.Config
{
    public class AcademicStructureConfiguration : IEntityTypeConfiguration<Stage>
    {
        public void Configure(EntityTypeBuilder<Stage> builder)
        {
            builder.ToTable("Stages");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(s => s.Name)
                   .IsUnique();

            builder.Property(s => s.Description)
                   .HasMaxLength(500);

            builder.Property(s => s.OrderIndex)
                   .IsRequired();

            builder.Property(s => s.IsActive)
                   .HasDefaultValue(true);

            builder.Property(s => s.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
