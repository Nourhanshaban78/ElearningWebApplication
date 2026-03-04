using E_learning.Core.Entities.Assessments.Exams;
using E_learning.Core.Entities.Assessments.Quizzes;
using E_learning.Core.Entities.Billing___Payments;
using E_learning.Core.Entities.Courses___content;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string ProfileImage { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        //public Status IsActive { get; set; }
        public DateTime MemberSince { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<Courses> Courses { get; set; }

        public ICollection<ExamAttempts> ExamAttempts { get; set; }
        public ICollection<QuizAttempts> QuizAttempts { get; set; }

        public ICollection<InstructorEarnings> InstructorEarnings { get; set; }
        public ICollection<PaymentMethods> PaymentMethods { get; set; }
        public ICollection<PaymentTransactions> PaymentTransactions { get; set; }
        public ICollection<PayoutRequests> PayoutRequests { get; set; }
    }
}
