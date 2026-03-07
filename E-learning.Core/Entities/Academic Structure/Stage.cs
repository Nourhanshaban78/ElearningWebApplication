using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Academic_Structure
{
    
        public class Stage
        {
            public Guid Id { get; set; }

            public string Name { get; set; } = string.Empty;
            public string? Description { get; set; }
            public int OrderIndex { get; set; }
            public bool IsActive { get; set; } = true;
            public DateTime CreatedAt { get; set; }

            public ICollection<Level> Levels { get; set; } = new List<Level>();
        }
    }
