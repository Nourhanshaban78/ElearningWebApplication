using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Service.DTOs.Admin_Dashboard
{
    public class DashboardOverviewResponse
    {
        public int TotalPassedStudents { get; set; }
        public int TotalFailedStudents { get; set; }
        public decimal PassRateProgress { get; set; }
        public decimal FailRateComparison { get; set; }

        public List<MonthlyPerformanceTrend> monthlyTrends { get; set; } = new();
    }


    public class MonthlyPerformanceTrend
    {
        public string Month { get; set; } = string.Empty; // مثل "Jan", "Feb"
        public int PassedCount { get; set; }
        public int FailedCount { get; set; }
    }
}
