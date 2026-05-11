namespace HealthCare.Domain.Entities
{
    public class LabResult : BaseEntity
    {
        public int LabOrderId { get; set; }
        public string ResultValue { get; set; }
        public string NormalRange { get; set; }
        public string Remarks { get; set; }

        // Navigation property
        public LabOrder LabOrder { get; set; }
    }
}
