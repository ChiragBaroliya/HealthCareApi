using System;

namespace HealthCare.Domain.Entities
{
    public class PharmacyOrder : BaseEntity
    {
        public int PatientId { get; set; }
        public int PrescriptionId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } // e.g., Pending, Prepared, Dispatched, Delivered, Cancelled

        // Navigation properties
        public Patient Patient { get; set; }
        public Prescription Prescription { get; set; }
    }
}
