using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Service.DTOs.AssignmentsDto
{
    public class CreateSubmissionDto
    {
        public int AssignmentId { get; set; }
        public Guid StudentId { get; set; }

        public string? FileUrl { get; set; }
        public string? Notes { get; set; }
    }
}
