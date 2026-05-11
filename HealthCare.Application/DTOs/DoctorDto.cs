using System;
using System.Collections.Generic;

namespace HealthCare.Application.DTOs
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string DoctorCode { get; set; }
        public string FullName { get; set; }
        public int SpecializationId { get; set; }
        public string SpecializationName { get; set; }
        public string Qualification { get; set; }
        public int ExperienceYears { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public decimal ConsultationFee { get; set; }
        public string LicenseNumber { get; set; }
        public bool AvailabilityStatus { get; set; }
        public List<DoctorScheduleDto> Schedules { get; set; } = new();
    }

    public class CreateDoctorDto
    {
        public string FullName { get; set; }
        public int SpecializationId { get; set; }
        public string Qualification { get; set; }
        public int ExperienceYears { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public decimal ConsultationFee { get; set; }
        public string LicenseNumber { get; set; }
    }

    public class DoctorSpecializationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CreateSpecializationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class DoctorScheduleDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public string StartTime { get; set; } // Format: HH:mm:ss
        public string EndTime { get; set; }   // Format: HH:mm:ss
        public bool IsAvailable { get; set; }
    }
}
