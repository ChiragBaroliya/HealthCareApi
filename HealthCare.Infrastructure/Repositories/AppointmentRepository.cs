using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCare.Application.Repositories;
using HealthCare.Domain.Entities;
using HealthCare.Domain.Enums;
using HealthCare.Infrastructure.Data;
using HealthCare.Application.Common;
using HealthCare.Application.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly HealthCareDbContext _context;

        public AppointmentRepository(HealthCareDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToListAsync();
        }

        public async Task<PagedResult<Appointment>> GetPagedAsync(QueryParameters queryParams)
        {
            var query = _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(queryParams.SearchTerm))
            {
                query = query.Where(a => 
                    a.AppointmentNumber.Contains(queryParams.SearchTerm) || 
                    a.Patient.FirstName.Contains(queryParams.SearchTerm) || 
                    a.Patient.LastName.Contains(queryParams.SearchTerm) ||
                    a.Doctor.FullName.Contains(queryParams.SearchTerm));
            }

            // Sorting
            query = query.ApplySorting(queryParams.SortBy, queryParams.IsDescending);

            var totalCount = await query.CountAsync();

            // Pagination
            var appointments = await query
                .ApplyPagination(queryParams.PageNumber, queryParams.PageSize)
                .ToListAsync();

            return new PagedResult<Appointment>(appointments, totalCount, queryParams.PageNumber, queryParams.PageSize);
        }

        public async Task<Appointment> GetByIdAsync(int id)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Appointment>> GetByPatientIdAsync(int patientId)
        {
            return await _context.Appointments
                .Include(a => a.Doctor)
                .Where(a => a.PatientId == patientId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByDoctorIdAsync(int doctorId)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Where(a => a.DoctorId == doctorId)
                .ToListAsync();
        }

        public async Task<Appointment> AddAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateStatusAsync(int id, AppointmentStatus status)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                appointment.Status = status;
                await _context.SaveChangesAsync();
            }
        }
    }
}
