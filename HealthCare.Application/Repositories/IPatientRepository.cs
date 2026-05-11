using System.Collections.Generic;
using System.Threading.Tasks;
using HealthCare.Application.DTOs;
using HealthCare.Application.Models;
using HealthCare.Application.Common;

namespace HealthCare.Application.Repositories
{
    public interface IPatientRepository
    {
        Task<ResponseModel<IEnumerable<PatientDto>>> GetAllAsync();
        Task<ResponseModel<PagedResult<PatientDto>>> GetPagedAsync(QueryParameters queryParams);
        Task<ResponseModel<PatientDto>> GetByIdAsync(int id);
        Task<ResponseModel<PatientDto>> CreateAsync(PatientDto dto);
        Task<ResponseModel<PatientDto>> UpdateAsync(int id, PatientDto dto);
        Task<ResponseModel<bool>> DeleteAsync(int id);
    }
}
