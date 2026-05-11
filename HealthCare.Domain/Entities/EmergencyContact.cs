using System;

namespace HealthCare.Domain.Entities
{
    public class EmergencyContact : BaseEntity
    {
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public string FullName { get; set; }
        public string Relationship { get; set; }
        public string ContactNumber { get; set; }
    }
}
