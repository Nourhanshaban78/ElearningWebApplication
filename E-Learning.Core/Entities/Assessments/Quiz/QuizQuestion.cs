using E_Learning.Core.Base;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Core.Entities.Assessments.Quiz
{
    public class QuizQuestion : BaseEntity
    {
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; } = null!;

        public string Text { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public decimal Points { get; set; } = 1;
        public bool IsAIGenerated { get; set; } = false;
        public int OrderIndex { get; set; }

        public ICollection<QuizOption> Options { get; set; }
            = new List<QuizOption>();
    }
}
