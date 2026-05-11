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
    public class DoctorRepository : IDoctorRepository
    {
        private readonly HealthCareDbContext _context;

        public DoctorRepository(HealthCareDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _context.Doctors
                .Include(d => d.Specialization)
                .ToListAsync();
        }

        public async Task<PagedResult<Doctor>> GetPagedAsync(QueryParameters queryParams)
        {
            var query = _context.Doctors
                .Include(d => d.Specialization)
                .AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(queryParams.SearchTerm))
            {
                query = query.Where(d => 
                    d.FullName.Contains(queryParams.SearchTerm) || 
                    d.DoctorCode.Contains(queryParams.SearchTerm) || 
                    d.Specialization.Name.Contains(queryParams.SearchTerm));
            }

            // Sorting
            query = query.ApplySorting(queryParams.SortBy, queryParams.IsDescending);

            var totalCount = await query.CountAsync();

            // Pagination
            var doctors = await query
                .ApplyPagination(queryParams.PageNumber, queryParams.PageSize)
                .ToListAsync();

            return new PagedResult<Doctor>(doctors, totalCount, queryParams.PageNumber, queryParams.PageSize);
        }

        public async Task<Doctor> GetByIdAsync(int id)
        {
            return await _context.Doctors
                .Include(d => d.Specialization)
                .Include(d => d.Schedules)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Doctor> GetByCodeAsync(string code)
        {
            return await _context.Doctors
                .Include(d => d.Specialization)
                .FirstOrDefaultAsync(d => d.DoctorCode == code);
        }

        public async Task<Doctor> AddAsync(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
            return doctor;
        }

        public async Task UpdateAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<DoctorSpecialization>> GetSpecializationsAsync()
        {
            return await _context.DoctorSpecializations.ToListAsync();
        }

        public async Task<DoctorSpecialization> GetSpecializationByIdAsync(int id)
        {
            return await _context.DoctorSpecializations.FindAsync(id);
        }

        public async Task<DoctorSpecialization> AddSpecializationAsync(DoctorSpecialization specialization)
        {
            await _context.DoctorSpecializations.AddAsync(specialization);
            await _context.SaveChangesAsync();
            return specialization;
        }

        public async Task<IEnumerable<DoctorSchedule>> GetSchedulesByDoctorIdAsync(int doctorId)
        {
            return await _context.DoctorSchedules
                .Where(s => s.DoctorId == doctorId)
                .ToListAsync();
        }

        public async Task UpdateSchedulesAsync(int doctorId, IEnumerable<DoctorSchedule> schedules)
        {
            var existingSchedules = await _context.DoctorSchedules
                .Where(s => s.DoctorId == doctorId)
                .ToListAsync();

            _context.DoctorSchedules.RemoveRange(existingSchedules);
            
            foreach (var schedule in schedules)
            {
                schedule.DoctorId = doctorId;
                await _context.DoctorSchedules.AddAsync(schedule);
            }

            await _context.SaveChangesAsync();
        }
    }
}
