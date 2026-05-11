using System.Collections.Generic;
using System.Threading.Tasks;
using HealthCare.Domain.Entities;

namespace HealthCare.Application.Repositories
{
    public interface ILabRepository
    {
        Task<IEnumerable<LabTest>> GetAllTestsAsync();
        Task<LabTest> GetTestByIdAsync(int id);
        Task<LabTest> AddTestAsync(LabTest test);

        Task<LabOrder> GetOrderByIdAsync(int id);
        Task<IEnumerable<LabOrder>> GetOrdersByPatientIdAsync(int patientId);
        Task<LabOrder> AddOrderAsync(LabOrder order);
        Task UpdateOrderAsync(LabOrder order);

        Task<LabResult> AddResultAsync(LabResult result);
    }
}
