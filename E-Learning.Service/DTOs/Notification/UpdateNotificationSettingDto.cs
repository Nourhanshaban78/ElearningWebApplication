using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Service.DTOs.Notification
{
    public class UpdateNotificationSettingDto
    {
        public bool CourseAnnouncement { get; set; }
        public bool AssignmentReminder { get; set; }
        public bool ExamNotification { get; set; }
        public bool PlatformUpdates { get; set; }

        public bool InAppNotification { get; set; }
        public bool EmailNotification { get; set; }
    }
}
