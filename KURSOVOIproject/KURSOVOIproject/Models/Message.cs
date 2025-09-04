using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KURSOVOIproject.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public int SenderUserId { get; set; }
        public string Text { get; set; } = default!;
        public DateTime Timestamp { get; set; }

        // Навигация (необязательно)
        [ForeignKey("ApplicationId")]
        public ApplicationEntity Application { get; set; } = default!;
    }
}
