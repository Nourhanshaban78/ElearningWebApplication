using E_Learning.Core.Entities.Billing;
using E_Learning.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class InstructorEarningConfiguration
    : IEntityTypeConfiguration<InstructorEarning>
{
    public void Configure(EntityTypeBuilder<InstructorEarning> builder)
    {
        builder.ToTable("InstructorEarnings");
        builder.HasKey(ie => ie.Id);

        builder.Property(ie => ie.GrossAmount)
               .HasColumnType("decimal(10,2)");

        builder.Property(ie => ie.PlatformFee)
               .HasColumnType("decimal(10,2)");

        builder.Property(ie => ie.NetAmount)
               .HasColumnType("decimal(10,2)");

        builder.Property(ie => ie.Status)
               .HasConversion<string>()
               .HasMaxLength(20)
               .HasDefaultValue(EarningStatus.Pending);

        builder.Property(ie => ie.CreatedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        // Earning → Instructor
        builder.HasOne(ie => ie.Instructor)
               .WithMany()
               .HasForeignKey(ie => ie.InstructorId)
               .OnDelete(DeleteBehavior.Restrict);

        //builder.HasOne(ie => ie.Transaction)
        //       .WithOne(pt => pt.InstructorEarning)
        //       .HasForeignKey<InstructorEarning>(ie => ie.TransactionId)
        //       .OnDelete(DeleteBehavior.Restrict);

        // Earning → Course
        builder.HasOne(ie => ie.Course)
               .WithMany()
               .HasForeignKey(ie => ie.CourseId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}