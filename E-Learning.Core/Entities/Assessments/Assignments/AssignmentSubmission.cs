using E_learning.Core.Entities.Identity;
using E_Learning.Core.Base;
using E_Learning.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Core.Entities.Assessments.Assignments
{
    public class AssignmentSubmission : BaseEntity
    {
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; } = null!;

        public Guid StudentId { get; set; }
        public ApplicationUser Student { get; set; } = null!;

        public DateTime? SubmittedAt { get; set; }
        public string? FileUrl { get; set; }
        public string? Notes { get; set; }
        public decimal? Score { get; set; }
        public string? TeacherComment { get; set; }

        public AssignmentStatus Status { get; set; }
            = AssignmentStatus.Pending;
    }
}
