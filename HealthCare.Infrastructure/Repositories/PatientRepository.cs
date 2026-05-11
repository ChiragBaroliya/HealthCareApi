using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCare.Domain.Entities;
using HealthCare.Infrastructure.Data;
using HealthCare.Application.DTOs;
using HealthCare.Application.Models;
using HealthCare.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly HealthCareDbContext _db;

        public PatientRepository(HealthCareDbContext db)
        {
            _db = db;
        }

        public async Task<ResponseModel<IEnumerable<PatientDto>>> GetAllAsync()
        {
            var patients = await _db.Patients
                .Include(p => p.PatientAddresses)
                .Include(p => p.EmergencyContacts)
                .ToListAsync();

            var dtos = patients.Select(p => MapToDto(p)).ToList();
            return ResponseModel<IEnumerable<PatientDto>>.Ok(dtos, string.Empty);
        }

        public async Task<ResponseModel<PatientDto>> GetByIdAsync(int id)
        {
            var p = await _db.Patients
                .Include(pat => pat.PatientAddresses)
                .Include(pat => pat.EmergencyContacts)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (p == null) return ResponseModel<PatientDto>.Fail("Not found");

            return ResponseModel<PatientDto>.Ok(MapToDto(p), string.Empty);
        }

        public async Task<ResponseModel<PatientDto>> CreateAsync(PatientDto dto)
        {
            var entity = new Patient
            {
                PatientCode = dto.PatientCode,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth,
                BloodGroup = dto.BloodGroup,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                EmergencyContact = dto.EmergencyContact,
                MaritalStatus = dto.MaritalStatus,
                Nationality = dto.Nationality,
                MedicalHistory = dto.MedicalHistory,
                Allergies = dto.Allergies,
                InsuranceProvider = dto.InsuranceProvider,
                InsuranceNumber = dto.InsuranceNumber,
                IsActive = dto.IsActive,
            };

            _db.Patients.Add(entity);
            await _db.SaveChangesAsync();

            return ResponseModel<PatientDto>.Ok(MapToDto(entity));
        }

        public async Task<ResponseModel<PatientDto>> UpdateAsync(int id, PatientDto dto)
        {
            var entity = await _db.Patients.Include(p => p.PatientAddresses).Include(p => p.EmergencyContacts).FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return ResponseModel<PatientDto>.Fail("Not found");

            entity.PatientCode = dto.PatientCode;
            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;
            entity.Gender = dto.Gender;
            entity.DateOfBirth = dto.DateOfBirth;
            entity.BloodGroup = dto.BloodGroup;
            entity.Email = dto.Email;
            entity.PhoneNumber = dto.PhoneNumber;
            entity.Address = dto.Address;
            entity.EmergencyContact = dto.EmergencyContact;
            entity.MaritalStatus = dto.MaritalStatus;
            entity.Nationality = dto.Nationality;
            entity.MedicalHistory = dto.MedicalHistory;
            entity.Allergies = dto.Allergies;
            entity.InsuranceProvider = dto.InsuranceProvider;
            entity.InsuranceNumber = dto.InsuranceNumber;
            entity.IsActive = dto.IsActive;

            await _db.SaveChangesAsync();

            return ResponseModel<PatientDto>.Ok(MapToDto(entity));
        }

        public async Task<ResponseModel<bool>> DeleteAsync(int id)
        {
            var entity = await _db.Patients.FindAsync(id);
            if (entity == null) return ResponseModel<bool>.Fail("Not found");

            _db.Patients.Remove(entity);
            await _db.SaveChangesAsync();

            return ResponseModel<bool>.Ok(true, "Deleted");
        }

        private PatientDto MapToDto(Patient p)
        {
            return new PatientDto
            {
                Id = p.Id,
                PatientCode = p.PatientCode,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Gender = p.Gender,
                DateOfBirth = p.DateOfBirth,
                BloodGroup = p.BloodGroup,
                Email = p.Email,
                PhoneNumber = p.PhoneNumber,
                Address = p.Address,
                EmergencyContact = p.EmergencyContact,
                MaritalStatus = p.MaritalStatus,
                Nationality = p.Nationality,
                MedicalHistory = p.MedicalHistory,
                Allergies = p.Allergies,
                InsuranceProvider = p.InsuranceProvider,
                InsuranceNumber = p.InsuranceNumber,
                IsActive = p.IsActive,
                PatientAddresses = p.PatientAddresses.Select(pa => new HealthCare.Application.DTOs.PatientAddressDto
                {
                    Id = pa.Id,
                    PatientId = pa.PatientId,
                    AddressLine1 = pa.AddressLine1,
                    AddressLine2 = pa.AddressLine2,
                    City = pa.City,
                    State = pa.State,
                    Country = pa.Country,
                    ZipCode = pa.ZipCode
                }).ToList(),
                EmergencyContacts = p.EmergencyContacts.Select(ec => new HealthCare.Application.DTOs.EmergencyContactDto
                {
                    Id = ec.Id,
                    PatientId = ec.PatientId,
                    FullName = ec.FullName,
                    Relationship = ec.Relationship,
                    ContactNumber = ec.ContactNumber
                }).ToList()
            };
        }
    }
}
