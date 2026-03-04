using E_learning.Core.Entities.Base;
using E_learning.Core.Entities.Courses___content;
using E_learning.Core.Entities.Enrollment___Progress;
using E_learning.Core.Entities.Identity;
using E_learning.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Enrollment___Progress
{
  
    public class LessonProgress : BaseEntity
    {
        public int EnrollmentId { get; set; }
        public Enrollment Enrollment { get; set; } = null!;

        public int LessonId { get; set; }
        public Lessons Lesson { get; set; } = null!;

    
        public EnrollmentStatus Status { get; set; }
            = EnrollmentStatus.NotStarted;

        public int WatchedSeconds { get; set; } = 0;
        public DateTime? CompletedAt { get; set; }
        public DateTime LastAccessedAt { get; set; } = DateTime.UtcNow;
    }
}
