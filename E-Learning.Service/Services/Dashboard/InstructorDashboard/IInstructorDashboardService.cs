using E_Learning.Core.Base;
using E_Learning.Service.DTOs.Dashboard.Instructor_Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Service.Services.Dashboard.InstructorDashboard
{
    public interface IInstructorDashboardService
    {
        Task<Response<InstructorDashboardDto>> GetDashboardAsync( CancellationToken ct = default);
    }
}
