using System.Collections.Generic;

namespace HealthCare.Domain.Entities
{
    public class Hospital : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }

        // Navigation property
        public ICollection<Department> Departments { get; set; }

        public Hospital()
        {
            Departments = new HashSet<Department>();
        }
    }
}
