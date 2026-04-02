using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Learning.Service.DTOs.Enrollments.Enrollment;

namespace E_Learning.Service.DTOs.LiveSessionDto
{
  public class SessionAttendeesDashboardDto
{
    public LiveSessionResponseDto LiveSession { get; set; } // معلومات الجلسة (مرة واحدة)
    public List<AttendeeSummaryDto> Attendees { get; set; } // قائمة الطلاب الحاضرين
}

public class AttendeeSummaryDto
{
    public Guid StudentId { get; set; }
    public StudentSummaryDto Student { get; set; }
    public DateTime JoinedAt { get; set; }
    public DateTime? LeftAt { get; set; }
    public int? DurationSeconds { get; set; }
}
}