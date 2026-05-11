using System.Collections.Generic;
using System.Threading.Tasks;
using HealthCare.Application.DTOs;
using HealthCare.Application.Common;

namespace HealthCare.Application.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync();
        Task<PagedResult<DoctorDto>> GetPagedDoctorsAsync(QueryParameters queryParams);
        Task<DoctorDto> GetDoctorByIdAsync(int id);
        Task<DoctorDto> CreateDoctorAsync(CreateDoctorDto doctorDto);
        Task UpdateDoctorAsync(int id, CreateDoctorDto doctorDto);
        Task DeleteDoctorAsync(int id);

        Task<IEnumerable<DoctorSpecializationDto>> GetSpecializationsAsync();
        Task<DoctorSpecializationDto> CreateSpecializationAsync(CreateSpecializationDto specDto);

        Task UpdateDoctorScheduleAsync(int doctorId, IEnumerable<DoctorScheduleDto> scheduleDtos);
    }
}
