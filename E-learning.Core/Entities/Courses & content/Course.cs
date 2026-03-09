using E_learning.Core.Entities.Academic_Structure;
using E_learning.Core.Entities.Assessments.Assignments;
using E_learning.Core.Entities.Assessments.Exams;
using E_learning.Core.Entities.Assessments.Quizzes;
using E_learning.Core.Entities.Billing___Payments;
using E_learning.Core.Entities.Enrollment___Progress;
using E_learning.Core.Entities.Identity;
using E_learning.Core.Entities.LiveSessions;
using E_learning.Core.Entities.Profiles;
using E_learning.Core.Entities.Review_Certification_Schedule;
using E_learning.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace E_learning.Core.Entities.Courses___content
{
    public class Course
    {
        public Guid Id { get; set; }

        public Guid InstructorId { get; set; }
        public Instructor Instructor { get; set; }

        public Guid? LevelId { get; set; }
        public Level? Level { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string? WhatYouWillLearn { get; set; }

        public string? ThumbnailUrl { get; set; }
        public string Language { get; set; } = "en";
        public decimal Price { get; set; }
        public int? Duration { get; set; }
        public CoursesStatus Status { get; set; } = CoursesStatus.Draft;
        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Guid? ApprovedById { get; set; }
        public ApplicationUser? ApprovedBy { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public ICollection<LiveSession> LiveSessions { get; set; } = new List<LiveSession>();
        public ICollection<Section> Sections { get; set; } = new List<Section>();
        public ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
        public ICollection<PaymentTransaction> PaymentTransactions { get; set; } = new List<PaymentTransaction>();
        public ICollection<InstructorEarning> InstructorEarnings { get; set; } = new List<InstructorEarning>();
        public ICollection<CourseReview> CourseReviews { get; set; } = new List<CourseReview>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
        public ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();
        public ICollection<CourseAnalyticsSnapshots> AnalyticsSnapshots { get; set; }
    }
}
