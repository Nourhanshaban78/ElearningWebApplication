using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Service.DTOs.AssignmentsDto
{
    public class GradeSubmissionDto
    {
        public decimal Score { get; set; }
        public string? TeacherComment { get; set; }
    }
}
