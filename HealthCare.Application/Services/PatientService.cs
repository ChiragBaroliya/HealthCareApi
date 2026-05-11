using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCare.Application.DTOs;
using HealthCare.Application.Models;
using HealthCare.Application.Repositories;
using HealthCare.Application.Common;

namespace HealthCare.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repo;

        public PatientService(IPatientRepository repo)
        {
            _repo = repo;
        }

        public async Task<ResponseModel<IEnumerable<PatientDto>>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<ResponseModel<PagedResult<PatientDto>>> GetPagedAsync(QueryParameters queryParams)
        {
            return await _repo.GetPagedAsync(queryParams);
        }

        public async Task<ResponseModel<PatientDto>> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<ResponseModel<PatientDto>> CreateAsync(PatientDto dto)
        {
            return await _repo.CreateAsync(dto);
        }

        public async Task<ResponseModel<PatientDto>> UpdateAsync(int id, PatientDto dto)
        {
            return await _repo.UpdateAsync(id, dto);
        }

        public async Task<ResponseModel<bool>> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }
    }
}
