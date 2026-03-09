using E_Learning.Core.Entities.Academic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class StageConfiguration
    : IEntityTypeConfiguration<Stage>
{
    public void Configure(EntityTypeBuilder<Stage> builder)
    {
        builder.ToTable("Stages");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
               .HasMaxLength(100)
               .IsRequired();

        builder.HasIndex(s => s.Name)
               .IsUnique()
               .HasDatabaseName("UQ_Stage_Name");

        builder.Property(s => s.Description)
               .HasMaxLength(500);

        builder.Property(s => s.IsActive)
               .HasDefaultValue(true);

        builder.Property(s => s.CreatedAt)
               .HasDefaultValueSql("GETUTCDATE()");
    }
}