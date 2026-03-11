using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Learning.Core.Entities.Assessments.Assignments;
using E_Learning.Service.DTOs.AssignmentsDto;

namespace E_Learning.Service.Contract.Assignments
{
    public interface IAssignmentSubmissionService
    {
        Task<int> SubmitAsync(CreateSubmissionDto dto);

        Task GradeAsync(int submissionId, GradeSubmissionDto dto);

        Task<IReadOnlyList<AssignmentSubmission>> GetAllByAssignmentAsync(int assignmentId);

        Task<IReadOnlyList<AssignmentSubmission>>   GetByStudentAsync(Guid studentId);
    }
}
