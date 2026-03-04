using E_learning.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities
{
    public class Student
    {
        public int StudentId { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }

        public decimal EngagementRate { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }

        public ICollection<CourseReview> CourseReviews { get; set; }

        public ICollection<QuizAttempt> QuizAttempts { get; set; }

        public ICollection<ExamAttempt> ExamAttempts { get; set; }

        public ICollection<PaymentTransaction> PaymentTransactions { get; set; }

        public ICollection<Certificate> Certificates { get; set; }

        public ICollection<LessonProgress> LessonProgresses { get; set; }

        public ICollection<LiveSessionAttendance> LiveSessionAttendances { get; set; }
    }
}
