using System.Collections.Generic;
using System.Threading.Tasks;
using HealthCare.Domain.Entities;
using HealthCare.Application.Common;

namespace HealthCare.Application.Repositories
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task<PagedResult<Doctor>> GetPagedAsync(QueryParameters queryParams);
        Task<Doctor> GetByIdAsync(int id);
        Task<Doctor> GetByCodeAsync(string code);
        Task<Doctor> AddAsync(Doctor doctor);
        Task UpdateAsync(Doctor doctor);
        Task DeleteAsync(int id);

        Task<IEnumerable<DoctorSpecialization>> GetSpecializationsAsync();
        Task<DoctorSpecialization> GetSpecializationByIdAsync(int id);
        Task<DoctorSpecialization> AddSpecializationAsync(DoctorSpecialization specialization);
        
        Task<IEnumerable<DoctorSchedule>> GetSchedulesByDoctorIdAsync(int doctorId);
        Task UpdateSchedulesAsync(int doctorId, IEnumerable<DoctorSchedule> schedules);
    }
}
