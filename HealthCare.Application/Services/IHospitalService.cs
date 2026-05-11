using System.Collections.Generic;
using System.Threading.Tasks;
using HealthCare.Application.DTOs;

namespace HealthCare.Application.Services
{
    public interface IHospitalService
    {
        Task<IEnumerable<HospitalDto>> GetAllHospitalsAsync();
        Task<HospitalDto> GetHospitalByIdAsync(int id);
        Task<HospitalDto> CreateHospitalAsync(CreateHospitalDto hospitalDto);
        Task UpdateHospitalAsync(int id, CreateHospitalDto hospitalDto);
        Task DeleteHospitalAsync(int id);

        Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync(int hospitalId);
        Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto deptDto);
        Task DeleteDepartmentAsync(int id);
    }
}
