using System.Collections.Generic;
using System.Threading.Tasks;
using HealthCare.Application.DTOs;
using HealthCare.Domain.Enums;
using HealthCare.Application.Common;

namespace HealthCare.Application.Services
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync();
        Task<PagedResult<AppointmentDto>> GetPagedAppointmentsAsync(QueryParameters queryParams);
        Task<AppointmentDto> GetAppointmentByIdAsync(int id);
        Task<IEnumerable<AppointmentDto>> GetAppointmentsByPatientIdAsync(int patientId);
        Task<IEnumerable<AppointmentDto>> GetAppointmentsByDoctorIdAsync(int doctorId);
        Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto appointmentDto);
        Task UpdateAppointmentAsync(int id, CreateAppointmentDto appointmentDto);
        Task UpdateAppointmentStatusAsync(int id, AppointmentStatus status);
        Task DeleteAppointmentAsync(int id);
    }
}
