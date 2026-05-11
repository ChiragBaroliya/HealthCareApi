using System.Collections.Generic;
using System.Threading.Tasks;
using HealthCare.Application.DTOs;
using HealthCare.Application.Common;

namespace HealthCare.Application.Services
{
    public interface IPrescriptionService
    {
        Task<IEnumerable<PrescriptionDto>> GetAllPrescriptionsAsync();
        Task<PagedResult<PrescriptionDto>> GetPagedPrescriptionsAsync(QueryParameters queryParams);
        Task<PrescriptionDto> GetPrescriptionByIdAsync(int id);
        Task<IEnumerable<PrescriptionDto>> GetPrescriptionsByPatientIdAsync(int patientId);
        Task<IEnumerable<PrescriptionDto>> GetPrescriptionsByDoctorIdAsync(int doctorId);
        Task<PrescriptionDto> CreatePrescriptionAsync(CreatePrescriptionDto prescriptionDto);
        Task DeletePrescriptionAsync(int id);

        Task<IEnumerable<MedicineDto>> GetAllMedicinesAsync();
        Task<PagedResult<MedicineDto>> GetPagedMedicinesAsync(QueryParameters queryParams);
        Task<MedicineDto> GetMedicineByIdAsync(int id);
        Task<MedicineDto> CreateMedicineAsync(CreateMedicineDto medicineDto);
        Task UpdateMedicineAsync(int id, CreateMedicineDto medicineDto);
        Task DeleteMedicineAsync(int id);
    }
}
