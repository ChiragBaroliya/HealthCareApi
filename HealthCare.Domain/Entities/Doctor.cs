using System.Collections.Generic;

namespace HealthCare.Domain.Entities
{
    public class Doctor : BaseEntity
    {
        public string DoctorCode { get; set; }
        public string FullName { get; set; }
        public int SpecializationId { get; set; }
        public string Qualification { get; set; }
        public int ExperienceYears { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public decimal ConsultationFee { get; set; }
        public string LicenseNumber { get; set; }
        public bool AvailabilityStatus { get; set; }

        // Navigation properties
        public DoctorSpecialization Specialization { get; set; }
        public ICollection<DoctorSchedule> Schedules { get; set; }

        public Doctor()
        {
            Schedules = new HashSet<DoctorSchedule>();
        }
    }
}
