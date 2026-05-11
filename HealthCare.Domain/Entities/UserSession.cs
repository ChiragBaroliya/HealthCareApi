using System;

namespace HealthCare.Domain.Entities
{
    public class UserSession : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public string SessionId { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public DateTime? EndedAt { get; set; }
    }
}
