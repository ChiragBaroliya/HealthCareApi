using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCare.Application.Repositories;
using HealthCare.Domain.Entities;
using HealthCare.Infrastructure.Data;
using HealthCare.Application.Common;
using HealthCare.Application.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Infrastructure.Repositories
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly HealthCareDbContext _context;

        public PrescriptionRepository(HealthCareDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Prescription>> GetAllAsync()
        {
            return await _context.Prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Doctor)
                .Include(p => p.Appointment)
                .Include(p => p.PrescriptionMedicines)
                    .ThenInclude(pm => pm.Medicine)
                .ToListAsync();
        }

        public async Task<PagedResult<Prescription>> GetPagedAsync(QueryParameters queryParams)
        {
            var query = _context.Prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Doctor)
                .AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(queryParams.SearchTerm))
            {
                query = query.Where(p => 
                    p.Patient.FirstName.Contains(queryParams.SearchTerm) || 
                    p.Patient.LastName.Contains(queryParams.SearchTerm) ||
                    p.Doctor.FullName.Contains(queryParams.SearchTerm));
            }

            // Sorting
            query = query.ApplySorting(queryParams.SortBy, queryParams.IsDescending);

            var totalCount = await query.CountAsync();

            // Pagination
            var prescriptions = await query
                .ApplyPagination(queryParams.PageNumber, queryParams.PageSize)
                .ToListAsync();

            return new PagedResult<Prescription>(prescriptions, totalCount, queryParams.PageNumber, queryParams.PageSize);
        }

        public async Task<Prescription> GetByIdAsync(int id)
        {
            return await _context.Prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Doctor)
                .Include(p => p.Appointment)
                .Include(p => p.PrescriptionMedicines)
                    .ThenInclude(pm => pm.Medicine)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Prescription>> GetByPatientIdAsync(int patientId)
        {
            return await _context.Prescriptions
                .Include(p => p.Doctor)
                .Include(p => p.Appointment)
                .Include(p => p.PrescriptionMedicines)
                    .ThenInclude(pm => pm.Medicine)
                .Where(p => p.PatientId == patientId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Prescription>> GetByDoctorIdAsync(int doctorId)
        {
            return await _context.Prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Appointment)
                .Include(p => p.PrescriptionMedicines)
                    .ThenInclude(pm => pm.Medicine)
                .Where(p => p.DoctorId == doctorId)
                .ToListAsync();
        }

        public async Task<Prescription> AddAsync(Prescription prescription)
        {
            await _context.Prescriptions.AddAsync(prescription);
            await _context.SaveChangesAsync();
            return prescription;
        }

        public async Task DeleteAsync(int id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription != null)
            {
                _context.Prescriptions.Remove(prescription);
                await _context.SaveChangesAsync();
            }
        }
    }
}
