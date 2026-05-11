using System;
using System.Collections.Generic;

namespace HealthCare.Application.DTOs
{
    public class MedicineDto
    {
        public int Id { get; set; }
        public string MedicineName { get; set; }
        public string Manufacturer { get; set; }
        public string MedicineType { get; set; }
        public decimal UnitPrice { get; set; }
        public int StockQuantity { get; set; }
    }

    public class CreateMedicineDto
    {
        public string MedicineName { get; set; }
        public string Manufacturer { get; set; }
        public string MedicineType { get; set; }
        public decimal UnitPrice { get; set; }
        public int StockQuantity { get; set; }
    }

    public class PrescriptionDto
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public string AppointmentNumber { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public DateTime PrescriptionDate { get; set; }
        public string Notes { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public List<PrescriptionMedicineDto> Medicines { get; set; } = new();
    }

    public class PrescriptionMedicineDto
    {
        public int Id { get; set; }
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public string Duration { get; set; }
        public string Instructions { get; set; }
    }

    public class CreatePrescriptionDto
    {
        public int AppointmentId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public string Notes { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public List<CreatePrescriptionMedicineDto> Medicines { get; set; } = new();
    }

    public class CreatePrescriptionMedicineDto
    {
        public int MedicineId { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public string Duration { get; set; }
        public string Instructions { get; set; }
    }
}
