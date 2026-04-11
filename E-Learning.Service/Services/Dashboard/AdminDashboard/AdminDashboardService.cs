using AutoMapper;
using E_Learning.Core.Base;
using E_Learning.Core.Repository;
using E_Learning.Repository.Repositories;
using E_Learning.Service.DTOs.Admin_Dashboard;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Service.Services.Dashboard.AdminDashboard
{
    public class AdminDashboardService : IAdminDashboardService
    {
        private readonly IUnitOfWork _unit;
        private readonly ResponseHandler _response;
        private readonly IMapper _mapper;

        public AdminDashboardService(IUnitOfWork unit, ResponseHandler response, IMapper mapper)
        {
            _unit = unit;
            _response = response;
            _mapper = mapper;
        }

        public async Task<Response<DashboardOverviewResponse>> GetDashboardOverviewAsync(CancellationToken ct = default)
        {
            var completedAttemptsQuery = _unit.ExamAttempts.QueryNoTracking()
                .Where(a => a.SubmittedAt != null && a.IsPassed.HasValue);

            var totalPassedStudents = await completedAttemptsQuery.Where(a => a.IsPassed == true)
                .Select(a => a.StudentId).Distinct().CountAsync(ct);

            var totalFailedStudents = await completedAttemptsQuery.Where(a => a.IsPassed == false)
                .Select(a => a.StudentId).Distinct().CountAsync(ct);

            var totalDistcinctStudent = totalFailedStudents + totalPassedStudents;

            decimal passRate = totalDistcinctStudent > 0
                ? Math.Round((decimal)totalPassedStudents / totalDistcinctStudent * 100, 2) : 0;

            decimal failRate = totalDistcinctStudent > 0
                ? Math.Round((decimal)totalFailedStudents / totalDistcinctStudent * 100, 2) : 0;

            var currentYear = DateTime.UtcNow.Year;
            var trendsQuery = await completedAttemptsQuery
            .Where(a => a.SubmittedAt!.Value.Year == currentYear)
            .GroupBy(a => a.SubmittedAt!.Value.Month)
            .Select(g => new
            {
                MonthIndex = g.Key,
                PassedCount = g.Count(a => a.IsPassed == true),
                FailedCount = g.Count(a => a.IsPassed == false)
            })
            .ToListAsync(ct);

            var monthName = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            var monthlyTrends = new List<MonthlyPerformanceTrend>();

            for (int i = 1; i < 12; i++)
            {
                var monthData = trendsQuery.FirstOrDefault(a => a.MonthIndex == i);
                monthlyTrends.Add(new MonthlyPerformanceTrend
                {
                    Month = monthName[i - 1],
                    PassedCount = monthData?.PassedCount ?? 0,
                    FailedCount = monthData?.FailedCount ?? 0
                });                
            }
            var result = new DashboardOverviewResponse
            {
                TotalPassedStudents = totalPassedStudents,
                TotalFailedStudents = totalFailedStudents,
                PassRateProgress = passRate,
                FailRateComparison = failRate,
                monthlyTrends = monthlyTrends
            };
            return _response.Success(result);
        }
    }
}
