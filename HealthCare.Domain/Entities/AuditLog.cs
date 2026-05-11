using System;

namespace HealthCare.Domain.Entities
{
    public class AuditLog : BaseEntity
    {
        public string EntityName { get; set; }
        // Use int to reference entities' int Id
        public int EntityId { get; set; }
        public string Action { get; set; }
        public string PerformedBy { get; set; }
        public DateTime PerformedAt { get; set; } = DateTime.UtcNow;
        public string Changes { get; set; }
    }
}
