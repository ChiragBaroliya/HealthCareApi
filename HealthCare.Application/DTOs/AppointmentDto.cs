using System;
using HealthCare.Domain.Enums;

namespace HealthCare.Application.DTOs
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public string AppointmentNumber { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public AppointmentStatus Status { get; set; }
        public string Reason { get; set; }
        public string Notes { get; set; }
    }

    public class CreateAppointmentDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Reason { get; set; }
        public string Notes { get; set; }
    }

    public class UpdateAppointmentStatusDto
    {
        public AppointmentStatus Status { get; set; }
    }
}
