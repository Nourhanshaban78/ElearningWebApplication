using E_Learning.Core.Entities.Profiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class StudentProfileConfiguration
    : IEntityTypeConfiguration<StudentProfile>
{
    public void Configure(EntityTypeBuilder<StudentProfile> builder)
    {
        builder.ToTable("StudentProfiles");
        builder.HasKey(sp => sp.Id);

        // One-to-One مع AppUser
        builder.HasIndex(sp => sp.AppUserId)
               .IsUnique()
               .HasDatabaseName("UQ_StudentProfile_AppUser");

        builder.Property(sp => sp.EngagementRate)
               .HasColumnType("decimal(5,2)")
               .HasDefaultValue(0);

        builder.Property(sp => sp.CreatedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(sp => sp.AppUser)
               .WithOne()
               .HasForeignKey<StudentProfile>(sp => sp.AppUserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(sp => sp.Level)
               .WithMany()
               .HasForeignKey(sp => sp.LevelId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.SetNull);
    }
}