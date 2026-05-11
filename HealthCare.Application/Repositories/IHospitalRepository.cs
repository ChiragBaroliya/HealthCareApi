using System.Collections.Generic;
using System.Threading.Tasks;
using HealthCare.Domain.Entities;

namespace HealthCare.Application.Repositories
{
    public interface IHospitalRepository
    {
        Task<IEnumerable<Hospital>> GetAllAsync();
        Task<Hospital> GetByIdAsync(int id);
        Task<Hospital> AddAsync(Hospital hospital);
        Task UpdateAsync(Hospital hospital);
        Task DeleteAsync(int id);

        Task<IEnumerable<Department>> GetDepartmentsByHospitalIdAsync(int hospitalId);
        Task<Department> AddDepartmentAsync(Department department);
        Task DeleteDepartmentAsync(int id);
    }
}
