using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Repository.Config.Identity
{
    using E_learning.Core.Entities.Identity;
    using E_learning.Core.Entities.Notifactions;
    using E_learning.Core.Entities.Profiles;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x => x.FullName)
                .HasMaxLength(200);

            builder.HasOne(x => x.Student)
                .WithOne(x => x.User)
                .HasForeignKey<Student>(x => x.UserId);

            builder.HasOne(x => x.Instructor)
                .WithOne(x => x.User)
                .HasForeignKey<Instructor>(x => x.UserId);

            builder.HasOne(x => x.Admin)
                .WithOne(x => x.User)
                .HasForeignKey<Admin>(x => x.UserId);

            builder.HasOne(x => x.NotificationSettings)
                .WithOne(x => x.User)
                .HasForeignKey<NotificationSettings>(x => x.UserId);
        }
    }
}
