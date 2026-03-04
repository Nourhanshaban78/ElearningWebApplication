using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Courses___content
{
    public class Sections
    {
        public Guid Id { get; set; }

        public Guid CourseId { get; set; }
        public Courses Courses { get; set; }

        public string Title { get; set; }
        public int OrderIndex { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Lessons> Lessons { get; set; }
    }
}
