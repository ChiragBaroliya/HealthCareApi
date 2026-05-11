using System;
using HealthCare.Domain.Enums;

namespace HealthCare.Domain.Entities
{
    public class Notification : BaseEntity
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public NotificationType NotificationType { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedDate { get; set; }

        // Navigation property
        public User User { get; set; }
    }
}
