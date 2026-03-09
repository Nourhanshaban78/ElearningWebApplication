using E_Learning.Core.Entities.Profiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class InstructorProfileConfiguration
    : IEntityTypeConfiguration<InstructorProfile>
{
    public void Configure(EntityTypeBuilder<InstructorProfile> builder)
    {
        builder.ToTable("InstructorProfiles");
        builder.HasKey(ip => ip.Id);

        builder.HasIndex(ip => ip.AppUserId)
               .IsUnique()
               .HasDatabaseName("UQ_InstructorProfile_AppUser");

        builder.Property(ip => ip.Headline)
               .HasMaxLength(200);

        builder.Property(ip => ip.TotalEarnings)
               .HasColumnType("decimal(12,2)")
               .HasDefaultValue(0);

        builder.Property(ip => ip.PendingPayout)
               .HasColumnType("decimal(12,2)")
               .HasDefaultValue(0);

        builder.Property(ip => ip.IsPublic)
               .HasDefaultValue(true);

        builder.Property(ip => ip.IsVerified)
               .HasDefaultValue(false);

        builder.Property(ip => ip.CreatedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(ip => ip.AppUser)
               .WithOne()
               .HasForeignKey<InstructorProfile>(ip => ip.AppUserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}