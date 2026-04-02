using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Service.DTOs.Dashboard.Instructor_Dashboard
{
    public class WeeklyEngagementPointDto
    {
        public string Day { get; set; } = string.Empty;
        public double EngagementRate { get; set; }

    }
}
