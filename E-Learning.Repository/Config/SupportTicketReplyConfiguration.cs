using E_Learning.Core.Entities.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SupportTicketReplyConfiguration
    : IEntityTypeConfiguration<SupportTicketReply>
{
    public void Configure(EntityTypeBuilder<SupportTicketReply> builder)
    {
        builder.ToTable("SupportTicketReplies");
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Body).IsRequired();

        builder.Property(r => r.CreatedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        // Reply → Ticket
        builder.HasOne(r => r.Ticket)
               .WithMany(st => st.Replies)
               .HasForeignKey(r => r.TicketId)
               .OnDelete(DeleteBehavior.Cascade);

        // Reply → Sender
        builder.HasOne(r => r.Sender)
               .WithMany()
               .HasForeignKey(r => r.SenderId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}