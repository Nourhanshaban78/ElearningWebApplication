<<<<<<< AhmedAtef
﻿using E_learning.Core.Entities.Assessments.Exams;
using E_learning.Core.Entities.Assessments.Quizzes;
using E_learning.Core.Entities.Billing___Payments;
using E_learning.Core.Entities.Courses___content;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
=======
﻿using Microsoft.AspNetCore.Identity;

>>>>>>> main

namespace E_learning.Core.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
<<<<<<< AhmedAtef
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
=======
        public string? Bio { get; set; }
        public string? ProfileImage { get; set; }
        public string? Location { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime MemberSince { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        //// Preferences
        //public string Language { get; set; } = "en";
        //public string TimeZone { get; set; } = "UTC";
        //public bool ProfileVisibility { get; set; } = true;
        //public bool ShowProgressToOthers { get; set; } = true;

        //// Notifications
        //public bool NotifyInApp { get; set; } = true;
        //public bool NotifyEmail { get; set; } = true;
>>>>>>> main
    }
}
