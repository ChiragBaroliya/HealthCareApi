using System;
using System.Collections.Generic;

namespace HealthCare.Domain.Entities
{
    public class LabOrder : BaseEntity
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int? AppointmentId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } // e.g., Pending, SampleCollected, InProgress, Completed, Cancelled

        // Navigation properties
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public Appointment Appointment { get; set; }
        public ICollection<LabResult> LabResults { get; set; }

        public LabOrder()
        {
            LabResults = new HashSet<LabResult>();
        }
    }
}
