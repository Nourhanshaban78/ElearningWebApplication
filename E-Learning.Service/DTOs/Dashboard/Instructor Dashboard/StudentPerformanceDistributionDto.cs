using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Service.DTOs.Dashboard.Instructor_Dashboard
{
    public class StudentPerformanceDistributionDto
    {
        public int Excellent { get; set; }          // >= 90%
        public int VeryGood { get; set; }           // >= 75%
        public int Good { get; set; }               // >= 60%
        public int NeedsImprovement { get; set; }   // < 60%
        public int TotalEvaluated { get; set; }
    }
}
