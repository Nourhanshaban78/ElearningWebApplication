using E_learning.Core.Entities.Courses___content;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Academic_Structure
{
    public class Level
    {
        public Guid Id { get; set; }

        public Guid StageId { get; set; }
        public Stage Stage { get; set; } = null!;

        public string Name { get; set; } = string.Empty;

        public int OrderIndex { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; }

        public int CourseCount { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
