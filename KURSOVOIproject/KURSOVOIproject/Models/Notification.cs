using System;

namespace KURSOVOIproject.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int RecipientUserId { get; set; }   // Студент или Компания
        public string Title { get; set; } = default!;
        public string Message { get; set; } = default!;
        public DateTime ScheduledAt { get; set; }
        public bool IsSent { get; set; }
    }
}
