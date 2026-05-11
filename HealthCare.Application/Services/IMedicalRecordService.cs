using System.Collections.Generic;
using System.Threading.Tasks;
using HealthCare.Application.DTOs;

namespace HealthCare.Application.Services
{
    public interface IMedicalRecordService
    {
        Task<IEnumerable<MedicalRecordDto>> GetMedicalRecordsAsync(int patientId);
        Task<MedicalRecordDto> CreateMedicalRecordAsync(CreateMedicalRecordDto recordDto);

        Task<IEnumerable<PatientAllergyDto>> GetPatientAllergiesAsync(int patientId);
        Task<PatientAllergyDto> AddPatientAllergyAsync(PatientAllergyDto allergyDto);
        Task DeletePatientAllergyAsync(int id);

        Task<IEnumerable<VaccinationRecordDto>> GetVaccinationRecordsAsync(int patientId);
        Task<VaccinationRecordDto> AddVaccinationRecordAsync(VaccinationRecordDto vaccinationDto);
        Task DeleteVaccinationRecordAsync(int id);
    }
}
