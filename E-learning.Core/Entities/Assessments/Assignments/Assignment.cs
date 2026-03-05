using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_learning.Core.Entities.Courses___content;

namespace E_learning.Core.Entities.Assessments.Assignments
{
    public class Assignment
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public decimal TotalMarks { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

     
        public Guid CourseId { get; set; }
        public virtual Courses Courses { get; set; } = null!;
        public virtual ICollection<AssignmentSubmissions> AssignmentSubmissions { get; set; } = new List<AssignmentSubmissions>();
    }
}
