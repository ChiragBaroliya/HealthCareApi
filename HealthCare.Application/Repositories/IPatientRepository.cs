using System.Collections.Generic;
using System.Threading.Tasks;
using HealthCare.Application.DTOs;
using HealthCare.Application.Models;

namespace HealthCare.Application.Repositories
{
    public interface IPatientRepository
    {
        Task<ResponseModel<IEnumerable<PatientDto>>> GetAllAsync();
        Task<ResponseModel<PatientDto>> GetByIdAsync(int id);
        Task<ResponseModel<PatientDto>> CreateAsync(PatientDto dto);
        Task<ResponseModel<PatientDto>> UpdateAsync(int id, PatientDto dto);
        Task<ResponseModel<bool>> DeleteAsync(int id);
    }
}
