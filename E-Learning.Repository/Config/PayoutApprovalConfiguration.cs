using E_Learning.Core.Entities.Billing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PayoutApprovalConfiguration
    : IEntityTypeConfiguration<PayoutApproval>
{
    public void Configure(EntityTypeBuilder<PayoutApproval> builder)
    {
        builder.ToTable("PayoutApprovals");
        builder.HasKey(pa => pa.Id);

        builder.Property(pa => pa.Decision)
               .HasMaxLength(20);

        builder.Property(pa => pa.ProcessedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        // One-to-One مع PayoutRequest
        builder.HasOne(pa => pa.PayoutRequest)
               .WithOne(pr => pr.PayoutApproval)
               .HasForeignKey<PayoutApproval>(pa => pa.PayoutRequestId)
               .OnDelete(DeleteBehavior.Restrict);

        // PayoutApproval → Admin
        builder.HasOne(pa => pa.Admin)
               .WithMany()
               .HasForeignKey(pa => pa.AdminId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}