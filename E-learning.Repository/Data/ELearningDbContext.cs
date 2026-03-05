using E_learning.Core.Entities.Base;
using E_learning.Core.Entities.Courses___content;
using E_learning.Core.Entities;
using E_learning.Core.Entities.AdminOperations;
using E_learning.Core.Entities.Enrollment___Progress;
using E_learning.Core.Entities.Identity;
using E_learning.Core.Entities.Review_Certification_Schedule;
using E_learning.Core.Entities.Notifactions;
using E_learning.Repository.Interceptors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;
using E_learning.Core.Entities.Academic_Structure;


namespace E_learning.Repository.Data
{
    public class ELearningDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ELearningDbContext(DbContextOptions<ELearningDbContext> options ) : base(options) {
        }


        #region DbSet
        #region Courses & Content
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Lessons> Lessons { get; set; }
        public DbSet<Sections> Sections { get; set; }
        #endregion

        #region Enrollment
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<LessonProgress> LessonProgress { get; set; }
        #endregion

        #region Identity
        public DbSet<OtpCodes> OtpCodes { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        #endregion
        #region Admin Operations
        public DbSet<PayoutApprovals> PayoutApprovals { get; set; }
        public DbSet<SupportTickets> SupportTickets { get; set; }
        public DbSet<SupportTicketReplies> SupportTicketReplies { get; set; }
        public DbSet<CourseAnalyticsSnapshots> CourseAnalyticsSnapshots { get; set; }
        #endregion
        #region Notifications & NotificationsSettings

        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<NotificationSettings> NotificationSettings { get; set; }

        #endregion
        #region Academic Structure
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Level> Levels { get; set; }
        #endregion
        #region Review&Certification&Schedule
        public DbSet<CourseReview> CourseReviews { get; set; }

        public DbSet<Certificate> Certificates { get; set; }

        public DbSet<ScheduleEvent> ScheduleEvents { get; set; }

        public DbSet<StudyReminder> StudyReminders { get; set; }
        #endregion
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ELearningDbContext).Assembly);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(parameter, nameof(ISoftDelete.IsDeleted));

                    var filter = Expression.Lambda(
                        Expression.Equal(property, Expression.Constant(false)),
                        parameter);

                    entityType.SetQueryFilter(filter);
                }
            }
        }

    }
}
