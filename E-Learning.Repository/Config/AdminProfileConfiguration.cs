using E_Learning.Core.Entities.Profiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AdminProfileConfiguration
    : IEntityTypeConfiguration<AdminProfile>
{
    public void Configure(EntityTypeBuilder<AdminProfile> builder)
    {
        builder.ToTable("AdminProfiles");
        builder.HasKey(ap => ap.Id);

        builder.HasIndex(ap => ap.AppUserId)
               .IsUnique()
               .HasDatabaseName("UQ_AdminProfile_AppUser");

        builder.Property(ap => ap.IsSuperAdmin)
               .HasDefaultValue(false);

        builder.Property(ap => ap.CreatedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(ap => ap.AppUser)
               .WithOne()
               .HasForeignKey<AdminProfile>(ap => ap.AppUserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}