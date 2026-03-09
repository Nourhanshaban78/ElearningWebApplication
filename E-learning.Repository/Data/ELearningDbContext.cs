using E_learning.Core.Entities.Base;
using E_learning.Core.Entities.Courses___content;
using E_learning.Core.Entities;
using E_learning.Core.Entities.AdminOperations;
using E_learning.Core.Entities.Enrollment___Progress;
using E_learning.Core.Entities.Identity;
using E_learning.Core.Entities.Profiles;
using E_learning.Core.Entities.Review_Certification_Schedule;
using E_learning.Core.Entities.Notifactions;
using E_learning.Repository.Interceptors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;
using E_learning.Core.Entities.Academic_Structure;
using E_learning.Core.Entities.Assessments.Assignments;
using E_learning.Core.Entities.Assessments.Exams;
using E_learning.Core.Entities.Assessments.Quizzes;
using E_learning.Core.Entities.Billing___Payments;
using E_learning.Core.Entities.LiveSessions;


namespace E_learning.Repository.Data
{
    public class ELearningDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ELearningDbContext(DbContextOptions<ELearningDbContext> options ) : base(options) {
        }


        #region DbSet
        #region Courses & Content
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Section> Sections { get; set; }
        #endregion

        #region Enrollment
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<LessonProgress> LessonProgress { get; set; }
        #endregion

        #region Identity
        public DbSet<OtpCode> OtpCodes { get; set; }
        public DbSet <UserSession> UserSessions { get; set; }
        #endregion
        #region Profile
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<Admin> Admins { get; set; }

        #endregion
        #region Admin Operations
        public DbSet<PayoutApprovals> PayoutApprovals { get; set; }
        public DbSet<SupportTickets> SupportTickets { get; set; }
        public DbSet<SupportTicketReplies> SupportTicketReplies { get; set; }
        public DbSet<CourseAnalyticsSnapshots> CourseAnalyticsSnapshots { get; set; }
        #endregion
        #region Notifications & NotificationsSettings

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationSetting> NotificationSettings { get; set; }

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
        #region Live sessions 
        public DbSet<LiveSession> LiveSessions { get; set; }
        public DbSet<LiveSessionAttendee> LiveSessionAttendees { get; set; }
         #endregion

        #region Payment 
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<InstructorEarning> InstructorEarnings { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
        public DbSet<PayoutRequest> PayoutRequests { get; set; }
         #endregion

        #region Assesments
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<AssignmentSubmission> AssignmentSubmissions { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<QuizOption> QuizOptions { get; set; }
        public DbSet<QuizAttempt> QuizAttempts { get; set; }
        public DbSet<QuizAttemptAnswer> QuizAttemptAnswers { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamQuestion> ExamQuestions { get; set; }
        public DbSet<ExamOption> ExamOptions { get; set; }
        public DbSet<ExamAttempt> ExamAttempts { get; set; }
        public DbSet<ExamAttemptAnswer> ExamAttemptAnswers { get; set; }
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
