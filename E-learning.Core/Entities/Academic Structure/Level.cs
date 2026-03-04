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
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("Stage")]
        public Guid StageId { get; set; }

        public Stage? Stage { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int OrderIndex { get; set; }

        public bool IsActive { get; set; }=true;

        public DateTime CreatedAt { get; set; }

        [NotMapped]
        public int CourseCount { get; set; }


    }
}
