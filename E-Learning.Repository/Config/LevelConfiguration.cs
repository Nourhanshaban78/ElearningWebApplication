using E_Learning.Core.Entities.Academic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class LevelConfiguration
    : IEntityTypeConfiguration<Level>
{
    public void Configure(EntityTypeBuilder<Level> builder)
    {
        builder.ToTable("Levels");
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Name)
               .HasMaxLength(100)
               .IsRequired();

        builder.HasIndex(l => new { l.StageId, l.Name })
               .IsUnique()
               .HasDatabaseName("UQ_Level_Stage_Name");

        builder.Property(l => l.IsActive)
               .HasDefaultValue(true);

        builder.Property(l => l.CreatedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(l => l.Stage)
               .WithMany(s => s.Levels)
               .HasForeignKey(l => l.StageId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}