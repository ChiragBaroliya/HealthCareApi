namespace HealthCare.Domain.Entities
{
    public class PrescriptionMedicine : BaseEntity
    {
        public int PrescriptionId { get; set; }
        public int MedicineId { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public string Duration { get; set; }
        public string Instructions { get; set; }

        // Navigation properties
        public Prescription Prescription { get; set; }
        public Medicine Medicine { get; set; }
    }
}
