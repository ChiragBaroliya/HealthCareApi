using System;

namespace HealthCare.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } // Kept as CreatedAt for compatibility
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; } // Kept as UpdatedAt for compatibility
        public bool IsDeleted { get; set; }

        protected BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
            CreatedBy = string.Empty;
            UpdatedBy = string.Empty;
            IsDeleted = false;
        }
    }
}
