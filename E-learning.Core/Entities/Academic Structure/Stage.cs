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
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public int OrderIndex { get; set; }

        public bool IsActive { get; set; }= true;

        public DateTime CreatedAt { get; set; }

        // Navigation Property (One-to-Many)
        public ICollection<Level> Levels { get; set; } = new List<Level>();
    }
}
