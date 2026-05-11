using System;
using System.Collections.Generic;

namespace HealthCare.Application.DTOs
{
    public class LabTestDto
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
    }

    public class CreateLabTestDto
    {
        public string TestName { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
    }

    public class LabOrderDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int? AppointmentId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public List<LabResultDto> Results { get; set; } = new();
    }

    public class CreateLabOrderDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int? AppointmentId { get; set; }
    }

    public class LabResultDto
    {
        public int Id { get; set; }
        public int LabOrderId { get; set; }
        public string ResultValue { get; set; }
        public string NormalRange { get; set; }
        public string Remarks { get; set; }
    }

    public class CreateLabResultDto
    {
        public int LabOrderId { get; set; }
        public string ResultValue { get; set; }
        public string NormalRange { get; set; }
        public string Remarks { get; set; }
    }
}
