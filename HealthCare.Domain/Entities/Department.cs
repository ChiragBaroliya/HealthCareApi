namespace HealthCare.Domain.Entities
{
    public class Department : BaseEntity
    {
        public int HospitalId { get; set; }
        public string DepartmentName { get; set; }
        public string Description { get; set; }

        // Navigation property
        public Hospital Hospital { get; set; }
    }
}
