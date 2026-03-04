using E_learning.Core.Entities.Assessments.Exams;
using E_learning.Core.Entities.Assessments.Quizzes;
using E_learning.Core.Entities.Billing___Payments;
using E_learning.Core.Entities.Enrollment___Progress;
using E_learning.Core.Entities.Identity;
using E_learning.Core.Entities.Review_Certification_Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Profiles
{
    public class Student
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        public decimal EngagementRate { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }

        public ICollection<CourseReview> CourseReviews { get; set; }

        public ICollection<QuizAttempts> QuizAttempts { get; set; }

        public ICollection<ExamAttempts> ExamAttempts { get; set; }

        public ICollection<PaymentTransactions> PaymentTransactions { get; set; }

        public ICollection<Certificate> Certificates { get; set; }

        public ICollection<LessonProgress> LessonProgresses { get; set; }

        //public ICollection<LiveSessionAttendance> LiveSessionAttendances { get; set; }
    }
}
