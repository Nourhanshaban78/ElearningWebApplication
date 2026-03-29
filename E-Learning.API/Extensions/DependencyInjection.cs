using E_Learning.API.Services;
using E_Learning.Service.Services.QuizServices;
using E_Learning.Service.Services.Schedule;

namespace E_Learning.API.Extensions
{
    public static  class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            // UnitOfWork
          //  Services.AddScoped<IUnitOfWork, UnitOfWork>();

          //  // Services
          //  Services.AddScoped<IStageService, StageService>();
          //  Services.AddScoped<ILevelService, LevelService>();
          //  // Enrollment Services
          // Services.AddScoped<IEnrollmentService, EnrollmentService>();
          // Services.AddScoped<ILessonProgressService, LessonProgressService>();
          // Services.AddScoped<IAssignmentService, AssignmentService>();
          // Services.AddScoped<IAssignmentSubmissionService, AssignmentSubmissionService>();
          // Services.AddScoped<IFileService, FileService>();
          // //enralment Repositories
          // Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
          // Services.AddScoped<ILessonProgressRepository, LessonProgressRepository>();
          //// notfications Services
          // Services.AddScoped<INotificationService, NotificationService>();
          // Services.AddScoped<INotificationSettingService, NotificationSettingService>();
          //  // Add services to the container.
          // Services.AddScoped<IStageService, StageService>();
          // Services.AddScoped<ILevelService, LevelService>();
          //  // Enrollment Services
          // Services.AddScoped<IEnrollmentService, EnrollmentService>();
          // Services.AddScoped<ILessonProgressService, LessonProgressService>();
          // Services.AddScoped<IAssignmentService, AssignmentService>();
          // Services.AddScoped<IAssignmentSubmissionService, AssignmentSubmissionService>();
          // Services.AddScoped<IFileService, FileService>();
          //  // Enrollment Repositories
          // Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
          // Services.AddScoped<ILessonProgressRepository, LessonProgressRepository>();
          //  // Notifications Services
          // Services.AddScoped<INotificationService, NotificationService>();
          // Services.AddScoped<INotificationSettingService, NotificationSettingService>();
          //  // CourseService
          // Services.AddScoped<ICourseContentService, CourseContentService>();
          // Services.AddScoped<ICourseService, CourseService>();
          // Services.AddApplicationServices();
          //  // AddApplicationServices
          // Services.AddScoped<INotificationHubService, NotificationHubService>();  // ← ضيف السطر ده
          // Services.AddScoped<IScheduleService, ScheduleService>();

          // Services.AddTransient<ResponseHandler>();
          // Services.AddScoped<IAdminService, AdminService>();
          // Services.AddScoped<IInstructorService, InstructorService>();
          // Services.AddScoped<IStudentService, StudentService>();


          // Services.AddScoped<ILiveSessionService, LiveSessionService>();
          // Services.AddScoped<ILiveSessionAttendeeService, LiveSessionAttendeeService>();

          // Services.AddScoped<ILiveSessionRepository, LiveSessionRepository>();
          // Services.AddScoped<ILiveSessionAttendeeRepository, LiveSessionAttendeeRepository>();
          // Services.AddScoped<IAdminProfileRepository, AdminProfileRepository>();
          // Services.AddScoped<IInstructorProfileRepository, InstructorProfileRepository>();
          // Services.AddScoped<IStudentProfileRepository, StudentProfileRepository>();
          // Services.AddScoped<ICertificateServices, CertificateServices>();
          // Services.AddScoped<IExamAttemptServices, ExamAttemptServices>();
          // Services.AddScoped<IExamServices, ExamServices>();
          // Services.AddScoped<IExamQuestionServices, ExamQuestionServices>();
          // Services.AddScoped<IExamAttemptAnswerServices, ExamAttemptAnswerServices>();
          // Services.AddScoped<IQuizService, QuizService>();



            return Services;
        }
    }
}
