using E_learning.Core.Entities.Assessments.Quizzes;
using E_learning.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Courses___content
{
    public class Lessons
    {
        public Guid Id { get; set; }

        public Guid SectionId { get; set; }
        public Sections Sections { get; set; }

        public string Title { get; set; }
        public LessonsType Type { get; set; }
        public string VideoUrl { get; set; }
        public string Content { get; set; }
        public int DurationSeconds { get; set; }
        public int OrderIndex { get; set; }
        public bool IsFreePreview { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        public ICollection<Quizzes> Quizzes { get; set; }
    }
}
