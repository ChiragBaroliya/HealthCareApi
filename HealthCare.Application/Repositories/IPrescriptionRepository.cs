using System.Collections.Generic;
using System.Threading.Tasks;
using HealthCare.Domain.Entities;
using HealthCare.Application.Common;

namespace HealthCare.Application.Repositories
{
    public interface IPrescriptionRepository
    {
        Task<IEnumerable<Prescription>> GetAllAsync();
        Task<PagedResult<Prescription>> GetPagedAsync(QueryParameters queryParams);
        Task<Prescription> GetByIdAsync(int id);
        Task<IEnumerable<Prescription>> GetByPatientIdAsync(int patientId);
        Task<IEnumerable<Prescription>> GetByDoctorIdAsync(int doctorId);
        Task<Prescription> AddAsync(Prescription prescription);
        Task DeleteAsync(int id);
    }
}
