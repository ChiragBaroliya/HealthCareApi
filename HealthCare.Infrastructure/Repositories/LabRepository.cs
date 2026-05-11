using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCare.Application.Repositories;
using HealthCare.Domain.Entities;
using HealthCare.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Infrastructure.Repositories
{
    public class LabRepository : ILabRepository
    {
        private readonly HealthCareDbContext _context;

        public LabRepository(HealthCareDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LabTest>> GetAllTestsAsync()
        {
            return await _context.LabTests.ToListAsync();
        }

        public async Task<LabTest> GetTestByIdAsync(int id)
        {
            return await _context.LabTests.FindAsync(id);
        }

        public async Task<LabTest> AddTestAsync(LabTest test)
        {
            await _context.LabTests.AddAsync(test);
            await _context.SaveChangesAsync();
            return test;
        }

        public async Task<LabOrder> GetOrderByIdAsync(int id)
        {
            return await _context.LabOrders
                .Include(lo => lo.Patient)
                .Include(lo => lo.Doctor)
                .Include(lo => lo.LabResults)
                .FirstOrDefaultAsync(lo => lo.Id == id);
        }

        public async Task<IEnumerable<LabOrder>> GetOrdersByPatientIdAsync(int patientId)
        {
            return await _context.LabOrders
                .Include(lo => lo.Doctor)
                .Where(lo => lo.PatientId == patientId)
                .OrderByDescending(lo => lo.OrderDate)
                .ToListAsync();
        }

        public async Task<LabOrder> AddOrderAsync(LabOrder order)
        {
            await _context.LabOrders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task UpdateOrderAsync(LabOrder order)
        {
            _context.LabOrders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task<LabResult> AddResultAsync(LabResult result)
        {
            await _context.LabResults.AddAsync(result);
            await _context.SaveChangesAsync();
            return result;
        }
    }
}
