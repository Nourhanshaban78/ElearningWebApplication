using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Service.DTOs.Dashboard.Instructor_Dashboard
{
    public class DashboardOverviewStatsDto
    {
        public int TotalCourses { get; set; }
        public int TotalStudents { get; set; }
        public int ActiveExams { get; set; }
        public int ActiveQuizzes { get; set; }

    }
}
