using System;

namespace HealthCare.Domain.Entities
{
    public class VaccinationRecord : BaseEntity
    {
        public int PatientId { get; set; }
        public string VaccineName { get; set; }
        public DateTime VaccinationDate { get; set; }
        public int DoseNumber { get; set; }

        // Navigation property
        public Patient Patient { get; set; }
    }
}
