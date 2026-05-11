using System.Collections.Generic;
using System.Threading.Tasks;
using HealthCare.Domain.Entities;
using HealthCare.Application.Common;

namespace HealthCare.Application.Repositories
{
    public interface IMedicineRepository
    {
        Task<IEnumerable<Medicine>> GetAllAsync();
        Task<PagedResult<Medicine>> GetPagedAsync(QueryParameters queryParams);
        Task<Medicine> GetByIdAsync(int id);
        Task<Medicine> AddAsync(Medicine medicine);
        Task UpdateAsync(Medicine medicine);
        Task DeleteAsync(int id);
        Task UpdateStockAsync(int id, int quantity);
    }
}
