using System;

namespace HealthCare.Application.DTOs
{
    public class MedicalRecordDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string Diagnosis { get; set; }
        public string Symptoms { get; set; }
        public string TreatmentPlan { get; set; }
        public string Notes { get; set; }
        public DateTime RecordDate { get; set; }
    }

    public class CreateMedicalRecordDto
    {
        public int PatientId { get; set; }
        public string Diagnosis { get; set; }
        public string Symptoms { get; set; }
        public string TreatmentPlan { get; set; }
        public string Notes { get; set; }
    }

    public class PatientAllergyDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string AllergyName { get; set; }
        public string Severity { get; set; }
        public string Notes { get; set; }
    }

    public class VaccinationRecordDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string VaccineName { get; set; }
        public DateTime VaccinationDate { get; set; }
        public int DoseNumber { get; set; }
    }
}
