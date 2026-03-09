// Configurations/SupportTicketConfiguration.cs
using E_Learning.Core.Entities.Support;
using E_Learning.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SupportTicketConfiguration
    : IEntityTypeConfiguration<SupportTicket>
{
    public void Configure(EntityTypeBuilder<SupportTicket> builder)
    {
        builder.ToTable("SupportTickets");
        builder.HasKey(st => st.Id);

        builder.Property(st => st.Subject)
               .HasMaxLength(200).IsRequired();

        builder.Property(st => st.Type)
               .HasMaxLength(20).IsRequired();

        builder.Property(st => st.Status)
               .HasConversion<string>()
               .HasMaxLength(20)
               .HasDefaultValue(SupportTicketStatus.Open);

        builder.HasOne(st => st.User)
               .WithMany()
               .HasForeignKey(st => st.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(st => st.AssignedAdmin)
               .WithMany()
               .HasForeignKey(st => st.AssignedTo)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.SetNull);
    }
}