using System.Collections.Generic;
using System.Threading.Tasks;
using HealthCare.Domain.Entities;

namespace HealthCare.Application.Repositories
{
    public interface IMedicalRecordRepository
    {
        Task<IEnumerable<MedicalRecord>> GetRecordsByPatientIdAsync(int patientId);
        Task<MedicalRecord> AddRecordAsync(MedicalRecord record);
        
        Task<IEnumerable<PatientAllergy>> GetAllergiesByPatientIdAsync(int patientId);
        Task<PatientAllergy> AddAllergyAsync(PatientAllergy allergy);
        Task DeleteAllergyAsync(int id);

        Task<IEnumerable<VaccinationRecord>> GetVaccinationsByPatientIdAsync(int patientId);
        Task<VaccinationRecord> AddVaccinationAsync(VaccinationRecord vaccination);
        Task DeleteVaccinationAsync(int id);
    }
}
