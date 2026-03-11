using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Learning.Core.Entities.Assessments.Assignments;
using E_Learning.Service.DTOs.AssignmentsDto;

namespace E_Learning.Service.Mapping
{
    public class AssignmentProfile : Profile
    {
        public AssignmentProfile()
        {
            CreateMap<CreateAssignmentDto, Assignment>();

            CreateMap<UpdateAssignmentDto, Assignment>();

            CreateMap<Assignment, AssignmentDto>();
        }
    }
}
