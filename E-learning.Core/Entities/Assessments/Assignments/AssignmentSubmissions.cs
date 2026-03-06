using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_learning.Core.Entities.Identity;
using E_learning.Core.Entities.Profiles;
using E_learning.Core.Enums;

namespace E_learning.Core.Entities.Assessments.Assignments
{
    public class AssignmentSubmissions
    {
        public Guid Id { get; set; }

        public DateTime? SubmittedAt { get; set; }
        public string? FileUrl { get; set; }
        public string? Notes { get; set; }
        public decimal? Score { get; set; }
        public string? TeacherComment { get; set; }
        public AssignmentStatus Status { get; set; } = AssignmentStatus.Pending;

        // Navigation properties
        public Guid AssignmentId { get; set; }
        public   Assignment Assignment { get; set; } = null!;
        public Guid StudentId { get; set; }
        public   Student Student { get; set; } = null!;
    }
}
