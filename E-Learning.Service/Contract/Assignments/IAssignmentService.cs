using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Learning.Service.DTOs.AssignmentsDto;

namespace E_Learning.Service.Contract.Assignments
{
    public interface IAssignmentService
    {
        Task<int> CreateAsync(CreateAssignmentDto dto);

        Task UpdateAsync(int id, UpdateAssignmentDto dto);

        Task DeleteAsync(int id);

        Task<AssignmentDto?> GetByIdAsync(int id);

        Task<IReadOnlyList<AssignmentDto>> GetAllAsync();
    }
}
