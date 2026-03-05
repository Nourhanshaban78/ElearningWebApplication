using E_learning.Core.Entities.Base;
using E_learning.Core.Entities.Enrollment___Progress;
using E_learning.Core.Entities.Identity;
using E_learning.Core.Entities.Review_Certification_Schedule;
using E_learning.Core.Entities.Notifactions;
using E_learning.Repository.Interceptors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using E_learning.Core.Entities.Assessments.Quizzes;
using E_learning.Core.Entities.Assessments.Exams;
using System.Linq.Expressions;


namespace E_learning.Repository.Data
{
    public class ELearningDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {

        private readonly AuditInterceptor _auditInterceptor;

        public ELearningDbContext(DbContextOptions<ELearningDbContext> options, AuditInterceptor auditInterceptor) : base(options)
        {
            _auditInterceptor = auditInterceptor;
        }

        #region DbSet

        #region Enrollment
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<LessonProgress> LessonProgress { get; set; }
        #endregion

        #region Identity
        public DbSet<OtpCodes> OtpCodes { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        #endregion

        #region Assessments 
        public DbSet<Quizzes> Quizzes { get; set; }
        public DbSet<QuizQuestions> QuizQuestions { get; set; }
        public DbSet<QuizOptions> QuizOptions { get; set; }
        public DbSet<QuizAttempts> QuizAttempts { get; set; }
        public DbSet<QuizAttemptAnswers> QuizAttemptAnswers { get; set; }

        public DbSet<Exams> Exams { get; set; }
        public DbSet<ExamQuestions> ExamQuestions { get; set; }
        public DbSet<ExamOptions> ExamOptions { get; set; }
        public DbSet<ExamAttempts> ExamAttempts { get; set; }
        public DbSet<ExamAttemptAnswers> ExamAttemptAnswers { get; set; }
        #endregion

        #region Notifications & NotificationsSettings

        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<NotificationSettings> NotificationSettings { get; set; }

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
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ELearningDbContext).Assembly);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(
                        entityType.ClrType, "e");

                    var property = Expression.Property(
                        parameter, nameof(ISoftDelete.IsDeleted));

                    var filter = Expression.Lambda(
                        Expression.Equal(
                            property,
                            Expression.Constant(false)),
                        parameter);

                    entityType.SetQueryFilter(filter);
                }
            }

            modelBuilder.Entity<NotificationSettings>()
                .HasKey(n => n.UserId);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.NotificationSettings)
                .WithOne(n => n.User)
                .HasForeignKey<NotificationSettings>(n => n.UserId);
        }



        // ─── OnConfiguring ───────────────────────
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditInterceptor);
        }


    }
}

