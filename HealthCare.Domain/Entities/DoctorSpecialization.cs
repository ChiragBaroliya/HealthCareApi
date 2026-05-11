using System.Collections.Generic;

namespace HealthCare.Domain.Entities
{
    public class DoctorSpecialization : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        // Navigation property
        public ICollection<Doctor> Doctors { get; set; }

        public DoctorSpecialization()
        {
            Doctors = new HashSet<Doctor>();
        }
    }
}
