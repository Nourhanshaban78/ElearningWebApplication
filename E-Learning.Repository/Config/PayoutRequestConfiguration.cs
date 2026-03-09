using E_Learning.Core.Entities.Billing;
using E_Learning.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PayoutRequestConfiguration
    : IEntityTypeConfiguration<PayoutRequest>
{
    public void Configure(EntityTypeBuilder<PayoutRequest> builder)
    {
        builder.ToTable("PayoutRequests");
        builder.HasKey(pr => pr.Id);

        builder.Property(pr => pr.Amount)
               .HasColumnType("decimal(10,2)");

        builder.Property(pr => pr.Method)
               .HasMaxLength(20);

        builder.Property(pr => pr.Status)
               .HasConversion<string>()
               .HasMaxLength(20)
               .HasDefaultValue(PayoutStatus.Pending);

        builder.Property(pr => pr.RequestedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        // PayoutRequest → Instructor
        builder.HasOne(pr => pr.Instructor)
               .WithMany()
               .HasForeignKey(pr => pr.InstructorId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}