using System.Collections.Generic;
using System.Threading.Tasks;
using HealthCare.Application.Repositories;
using HealthCare.Domain.Entities;
using HealthCare.Infrastructure.Data;
using HealthCare.Application.Common;
using HealthCare.Application.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Infrastructure.Repositories
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly HealthCareDbContext _context;

        public MedicineRepository(HealthCareDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Medicine>> GetAllAsync()
        {
            return await _context.Medicines.ToListAsync();
        }

        public async Task<PagedResult<Medicine>> GetPagedAsync(QueryParameters queryParams)
        {
            var query = _context.Medicines.AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(queryParams.SearchTerm))
            {
                query = query.Where(m => 
                    m.MedicineName.Contains(queryParams.SearchTerm) || 
                    m.Manufacturer.Contains(queryParams.SearchTerm));
            }

            // Sorting
            query = query.ApplySorting(queryParams.SortBy, queryParams.IsDescending);

            var totalCount = await query.CountAsync();

            // Pagination
            var medicines = await query
                .ApplyPagination(queryParams.PageNumber, queryParams.PageSize)
                .ToListAsync();

            return new PagedResult<Medicine>(medicines, totalCount, queryParams.PageNumber, queryParams.PageSize);
        }

        public async Task<Medicine> GetByIdAsync(int id)
        {
            return await _context.Medicines.FindAsync(id);
        }

        public async Task<Medicine> AddAsync(Medicine medicine)
        {
            await _context.Medicines.AddAsync(medicine);
            await _context.SaveChangesAsync();
            return medicine;
        }

        public async Task UpdateAsync(Medicine medicine)
        {
            _context.Medicines.Update(medicine);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine != null)
            {
                _context.Medicines.Remove(medicine);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateStockAsync(int id, int quantity)
        {
            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine != null)
            {
                medicine.StockQuantity += quantity;
                await _context.SaveChangesAsync();
            }
        }
    }
}
