using System.Collections.Generic;
using System.Threading.Tasks;
using HealthCare.Application.DTOs;

namespace HealthCare.Application.Services
{
    public interface ILabService
    {
        Task<IEnumerable<LabTestDto>> GetAllTestsAsync();
        Task<LabTestDto> CreateTestAsync(CreateLabTestDto testDto);

        Task<IEnumerable<LabOrderDto>> GetPatientOrdersAsync(int patientId);
        Task<LabOrderDto> CreateOrderAsync(CreateLabOrderDto orderDto);
        Task<LabResultDto> AddResultAsync(CreateLabResultDto resultDto);
    }
}
