using System;

namespace HealthCare.Domain.Entities
{
    public abstract class BaseEntity
    {
        // Use int for identity columns in database (auto-increment)
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        protected BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
