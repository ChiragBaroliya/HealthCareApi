using System;

namespace HealthCare.Domain.Entities
{
    public class MedicalRecord : BaseEntity
    {
        public int PatientId { get; set; }
        public string Diagnosis { get; set; }
        public string Symptoms { get; set; }
        public string TreatmentPlan { get; set; }
        public string Notes { get; set; }
        public DateTime RecordDate { get; set; }

        // Navigation property
        public Patient Patient { get; set; }
    }
}
