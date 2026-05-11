using System;
using System.Collections.Generic;

namespace HealthCare.Domain.Entities
{
    public class Prescription : BaseEntity
    {
        public int AppointmentId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime PrescriptionDate { get; set; }
        public string Notes { get; set; }
        public DateTime? FollowUpDate { get; set; }

        // Navigation properties
        public Appointment Appointment { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public ICollection<PrescriptionMedicine> PrescriptionMedicines { get; set; }

        public Prescription()
        {
            PrescriptionMedicines = new HashSet<PrescriptionMedicine>();
        }
    }
}
