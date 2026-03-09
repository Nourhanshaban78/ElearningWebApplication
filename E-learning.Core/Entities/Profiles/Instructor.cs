using E_learning.Core.Entities.Assessments.Exams;
using E_learning.Core.Entities.Assessments.Quizzes;
using E_learning.Core.Entities.Billing___Payments;
using E_learning.Core.Entities.Courses___content;
using E_learning.Core.Entities.Identity;
using E_learning.Core.Entities.LiveSessions;
using E_learning.Core.Entities.Review_Certification_Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Profiles

{
    public class Instructor
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public string? Headline { get; set; }
        public string? About { get; set; }

        public decimal TotalEarnings { get; set; }
        public decimal PendingPayout { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<PayoutRequest> PayoutRequests { get; set; } = new List<PayoutRequest>();
        public ICollection<Student> Students { get; set; }  = new List<Student>();
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
        public ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
        public ICollection<InstructorEarning> InstructorEarnings { get; set; } = new List<InstructorEarning>();
        public ICollection<LiveSession> LiveSessions { get; set; } = new List<LiveSession>();
        public ICollection<ScheduleEvent> ScheduleEvents { get; set; } = new List<ScheduleEvent>();
    }
}
