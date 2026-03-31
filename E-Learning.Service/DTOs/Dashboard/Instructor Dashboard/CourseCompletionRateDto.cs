using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Service.DTOs.Dashboard.Instructor_Dashboard
{
    public class CourseCompletionRateDto
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = string.Empty;
        public int TotalStudents { get; set; }
        public decimal CompletionRate { get; set; }

    }
}
