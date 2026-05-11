using System.Collections.Generic;
using System.Threading.Tasks;
using HealthCare.Domain.Entities;
using HealthCare.Domain.Enums;
using HealthCare.Application.Common;

namespace HealthCare.Application.Repositories
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAllAsync();
        Task<PagedResult<Appointment>> GetPagedAsync(QueryParameters queryParams);
        Task<Appointment> GetByIdAsync(int id);
        Task<IEnumerable<Appointment>> GetByPatientIdAsync(int patientId);
        Task<IEnumerable<Appointment>> GetByDoctorIdAsync(int doctorId);
        Task<Appointment> AddAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
        Task DeleteAsync(int id);
        Task UpdateStatusAsync(int id, AppointmentStatus status);
    }
}
