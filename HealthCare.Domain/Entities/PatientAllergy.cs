namespace HealthCare.Domain.Entities
{
    public class PatientAllergy : BaseEntity
    {
        public int PatientId { get; set; }
        public string AllergyName { get; set; }
        public string Severity { get; set; } // e.g., Low, Medium, High, Critical
        public string Notes { get; set; }

        // Navigation property
        public Patient Patient { get; set; }
    }
}
