using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCare.Application.Repositories;
using HealthCare.Domain.Entities;
using HealthCare.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Infrastructure.Repositories
{
    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private readonly HealthCareDbContext _context;

        public MedicalRecordRepository(HealthCareDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MedicalRecord>> GetRecordsByPatientIdAsync(int patientId)
        {
            return await _context.MedicalRecords
                .Include(mr => mr.Patient)
                .Where(mr => mr.PatientId == patientId)
                .OrderByDescending(mr => mr.RecordDate)
                .ToListAsync();
        }

        public async Task<MedicalRecord> AddRecordAsync(MedicalRecord record)
        {
            await _context.MedicalRecords.AddAsync(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<IEnumerable<PatientAllergy>> GetAllergiesByPatientIdAsync(int patientId)
        {
            return await _context.PatientAllergies
                .Where(pa => pa.PatientId == patientId)
                .ToListAsync();
        }

        public async Task<PatientAllergy> AddAllergyAsync(PatientAllergy allergy)
        {
            await _context.PatientAllergies.AddAsync(allergy);
            await _context.SaveChangesAsync();
            return allergy;
        }

        public async Task DeleteAllergyAsync(int id)
        {
            var allergy = await _context.PatientAllergies.FindAsync(id);
            if (allergy != null)
            {
                _context.PatientAllergies.Remove(allergy);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<VaccinationRecord>> GetVaccinationsByPatientIdAsync(int patientId)
        {
            return await _context.VaccinationRecords
                .Where(vr => vr.PatientId == patientId)
                .OrderByDescending(vr => vr.VaccinationDate)
                .ToListAsync();
        }

        public async Task<VaccinationRecord> AddVaccinationAsync(VaccinationRecord vaccination)
        {
            await _context.VaccinationRecords.AddAsync(vaccination);
            await _context.SaveChangesAsync();
            return vaccination;
        }

        public async Task DeleteVaccinationAsync(int id)
        {
            var vaccination = await _context.VaccinationRecords.FindAsync(id);
            if (vaccination != null)
            {
                _context.VaccinationRecords.Remove(vaccination);
                await _context.SaveChangesAsync();
            }
        }
    }
}
