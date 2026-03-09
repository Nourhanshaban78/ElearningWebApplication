using E_learning.Core.Entities.Identity;
using E_Learning.Core.Base;
using E_Learning.Core.Entities.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Core.Entities.Reviews
{
    public class CourseReview : BaseEntity
    {
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public Guid StudentId { get; set; }
        public ApplicationUser Student { get; set; } = null!;

        public byte Rating { get; set; }
        public string? Comment { get; set; }
        public string? InstructorReply { get; set; }
        public DateTime? InstructorRepliedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
