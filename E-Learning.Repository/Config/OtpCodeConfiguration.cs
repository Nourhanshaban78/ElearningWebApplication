using E_Learning.Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OtpCodeConfiguration
    : IEntityTypeConfiguration<OtpCode>
{
    public void Configure(EntityTypeBuilder<OtpCode> builder)
    {
        builder.ToTable("OtpCodes");
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Code)
               .HasMaxLength(10)
               .IsRequired();

        builder.Property(o => o.Purpose)
               .HasMaxLength(30)
               .IsRequired();

        builder.Property(o => o.IsUsed)
               .HasDefaultValue(false);

        builder.Property(o => o.CreatedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(o => o.User)
               .WithMany()
               .HasForeignKey(o => o.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}