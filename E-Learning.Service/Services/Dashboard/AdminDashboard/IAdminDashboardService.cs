using E_Learning.Core.Base;
using E_Learning.Service.DTOs.Admin_Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Service.Services.Dashboard.AdminDashboard
{
    public interface IAdminDashboardService
    {
        Task<Response<DashboardOverviewResponse>> GetDashboardOverviewAsync(CancellationToken ct = default);

        
    }
}
