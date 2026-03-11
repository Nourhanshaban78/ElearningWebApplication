using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Learning.Core.Entities.Assessments.Assignments;
using E_Learning.Core.Interfaces.Repositories;
using E_Learning.Core.Interfaces.Repositories.Assessments.Assignments;
using E_Learning.Service.Contract.Assignments;
using E_Learning.Service.DTOs.AssignmentsDto;

namespace E_Learning.Service.Services.AssignmentService
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _assignmentRepo;
        private readonly IMapper _mapper;

        public AssignmentService(
         IAssignmentRepository assignmentRepository,
            IMapper mapper)
        {
            _assignmentRepo = assignmentRepository;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(CreateAssignmentDto dto)
        {
            var assignment = _mapper.Map<Assignment>(dto);

            await _assignmentRepo.AddAsync(assignment);

            return assignment.Id;
        }

        public async Task UpdateAsync(int id, UpdateAssignmentDto dto)
        {
            var assignment = await _assignmentRepo.GetByIdAsync(id);

            if (assignment == null)
                throw new Exception("Assignment not found");

            _mapper.Map(dto, assignment);

            _assignmentRepo.Update(assignment);
        }

        public async Task DeleteAsync(int id)
        {
            var assignment = await _assignmentRepo.GetByIdAsync(id);

            if (assignment == null)
                throw new Exception("Assignment not found");

            _assignmentRepo.Remove(assignment);
        }

        public async Task<AssignmentDto?> GetByIdAsync(int id)
        {
            var assignment = await _assignmentRepo.GetByIdAsync(id);

            if (assignment == null)
                return null;

            return _mapper.Map<AssignmentDto>(assignment);
        }

        public async Task<IReadOnlyList<AssignmentDto>> GetAllAsync()
        {
            var assignments = await _assignmentRepo.GetAllAsync();

            return _mapper.Map<IReadOnlyList<AssignmentDto>>(assignments);
        }
    }
}
