using E_Learning.Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserSessionConfiguration
    : IEntityTypeConfiguration<UserSession>
{
    public void Configure(EntityTypeBuilder<UserSession> builder)
    {
        builder.ToTable("UserSessions");
        builder.HasKey(us => us.Id);

        builder.Property(us => us.RefreshToken)
               .HasMaxLength(512)
               .IsRequired();

        builder.HasIndex(us => us.RefreshToken)
               .IsUnique()
               .HasDatabaseName("UQ_UserSession_RefreshToken");

        builder.Property(us => us.DeviceInfo)
               .HasMaxLength(200);

        builder.Property(us => us.IpAddress)
               .HasMaxLength(50);

        builder.Property(us => us.IsActive)
               .HasDefaultValue(true);

        builder.Property(us => us.CreatedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(us => us.LastActivityAt)
               .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(us => us.User)
               .WithMany()
               .HasForeignKey(us => us.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}