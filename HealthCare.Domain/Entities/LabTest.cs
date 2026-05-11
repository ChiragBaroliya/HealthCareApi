namespace HealthCare.Domain.Entities
{
    public class LabTest : BaseEntity
    {
        public string TestName { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
    }
}
