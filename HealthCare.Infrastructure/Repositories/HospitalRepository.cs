using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCare.Application.Repositories;
using HealthCare.Domain.Entities;
using HealthCare.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Infrastructure.Repositories
{
    public class HospitalRepository : IHospitalRepository
    {
        private readonly HealthCareDbContext _context;

        public HospitalRepository(HealthCareDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Hospital>> GetAllAsync()
        {
            return await _context.Hospitals
                .Include(h => h.Departments)
                .ToListAsync();
        }

        public async Task<Hospital> GetByIdAsync(int id)
        {
            return await _context.Hospitals
                .Include(h => h.Departments)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<Hospital> AddAsync(Hospital hospital)
        {
            await _context.Hospitals.AddAsync(hospital);
            await _context.SaveChangesAsync();
            return hospital;
        }

        public async Task UpdateAsync(Hospital hospital)
        {
            _context.Hospitals.Update(hospital);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var hospital = await _context.Hospitals.FindAsync(id);
            if (hospital != null)
            {
                _context.Hospitals.Remove(hospital);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Department>> GetDepartmentsByHospitalIdAsync(int hospitalId)
        {
            return await _context.Departments
                .Where(d => d.HospitalId == hospitalId)
                .ToListAsync();
        }

        public async Task<Department> AddDepartmentAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            var dept = await _context.Departments.FindAsync(id);
            if (dept != null)
            {
                _context.Departments.Remove(dept);
                await _context.SaveChangesAsync();
            }
        }
    }
}
