using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Learning.Service.DTOs.Profiles.Student;

namespace E_Learning.Service.Services.UserDashboard
{
   public interface IDashboardService
    {
        Task<StudentDashboardDto> GetStudentDashboardDataAsync(Guid studentId);
    }
}