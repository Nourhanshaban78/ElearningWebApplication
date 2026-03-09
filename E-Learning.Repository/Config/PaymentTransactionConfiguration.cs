using E_Learning.Core.Entities.Billing;
using E_Learning.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PaymentTransactionConfiguration
    : IEntityTypeConfiguration<PaymentTransaction>
{
    public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
    {
        builder.ToTable("PaymentTransactions");
        builder.HasKey(pt => pt.Id);

        builder.Property(pt => pt.Amount)
               .HasColumnType("decimal(10,2)")
               .IsRequired();

        builder.Property(pt => pt.Currency)
               .HasMaxLength(5)
               .HasDefaultValue("USD");

        builder.Property(pt => pt.Status)
               .HasConversion<string>()
               .HasMaxLength(20)
               .HasDefaultValue(PaymentStatus.Pending);

        builder.Property(pt => pt.GatewayReference)
               .HasMaxLength(200);

        builder.Property(pt => pt.CreatedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        // Transaction → Student
        builder.HasOne(pt => pt.Student)
               .WithMany()
               .HasForeignKey(pt => pt.StudentId)
               .OnDelete(DeleteBehavior.Restrict);

        // Transaction → Course
        builder.HasOne(pt => pt.Course)
               .WithMany()
               .HasForeignKey(pt => pt.CourseId)
               .OnDelete(DeleteBehavior.Restrict);

        // Transaction → PaymentMethod (Optional)
        builder.HasOne(pt => pt.PaymentMethod)
               .WithMany()
               .HasForeignKey(pt => pt.PaymentMethodId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.SetNull);
    }
}