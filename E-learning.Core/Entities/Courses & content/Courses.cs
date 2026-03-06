using E_learning.Core.Entities.Academic_Structure;
using E_learning.Core.Entities.Assessments.Assignments;
using E_learning.Core.Entities.Assessments.Exams;
using E_learning.Core.Entities.Assessments.Quizzes;
using E_learning.Core.Entities.Billing___Payments;
using E_learning.Core.Entities.Identity;
using E_learning.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace E_learning.Core.Entities.Courses___content
{
    public class Courses
    {
        public Guid Id { get; set; }

        public Guid InstructorId { get; set; }
        public ApplicationUser Instructor { get; set; }

        public Guid? LevelId { get; set; }
        public Level Level { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string? WhatYouWillLearn { get; set; }

        public string? ThumbnailUrl { get; set; }
        public string Language { get; set; } = "en";
        public decimal Price { get; set; } = 0;
        public int? Duration { get; set; }
        public CoursesStatus Status { get; set; } = CoursesStatus.Draft;
        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Guid? ApprovedById { get; set; }   
        public ApplicationUser ApprovedBy { get; set; }   

        public DateTime ApprovedAt { get; set; }= DateTime.UtcNow;

        public ICollection<Sections> Sections { get; set; }
        public ICollection<Quizzes> Quizzes { get; set; }
        public ICollection<Exams> Exams { get; set; }
        public ICollection<PaymentTransactions> PaymentTransactions { get; set; }
        public ICollection<InstructorEarnings> InstructorEarnings { get; set; }


        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    }
}
