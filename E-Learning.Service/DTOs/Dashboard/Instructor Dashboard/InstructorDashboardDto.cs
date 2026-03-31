using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Service.DTOs.Dashboard.Instructor_Dashboard
{
    public class InstructorDashboardDto
    {
        public DashboardOverviewStatsDto Stats { get; set; } = new();
        public List<WeeklyEngagementPointDto> WeeklyEngagement { get; set; } = new();
        public StudentPerformanceDistributionDto PerformanceDistribution { get; set; } = new();
        public List<CourseCompletionRateDto> CourseCompletionRates { get; set; } = new();
        public decimal AverageCompletionRate { get; set; }
        public List<UpcomingLiveSessionDto> UpcomingLiveSessions { get; set; } = new();

    }
}
