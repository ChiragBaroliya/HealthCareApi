using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCare.Application.DTOs;
using HealthCare.Application.Repositories;
using HealthCare.Domain.Entities;

namespace HealthCare.Application.Services
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;

        public MedicalRecordService(IMedicalRecordRepository medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }

        public async Task<IEnumerable<MedicalRecordDto>> GetMedicalRecordsAsync(int patientId)
        {
            var records = await _medicalRecordRepository.GetRecordsByPatientIdAsync(patientId);
            return records.Select(mr => new MedicalRecordDto
            {
                Id = mr.Id,
                PatientId = mr.PatientId,
                PatientName = mr.Patient?.FirstName + " " + mr.Patient?.LastName,
                Diagnosis = mr.Diagnosis,
                Symptoms = mr.Symptoms,
                TreatmentPlan = mr.TreatmentPlan,
                Notes = mr.Notes,
                RecordDate = mr.RecordDate
            });
        }

        public async Task<MedicalRecordDto> CreateMedicalRecordAsync(CreateMedicalRecordDto recordDto)
        {
            var mr = new MedicalRecord
            {
                PatientId = recordDto.PatientId,
                Diagnosis = recordDto.Diagnosis,
                Symptoms = recordDto.Symptoms,
                TreatmentPlan = recordDto.TreatmentPlan,
                Notes = recordDto.Notes,
                RecordDate = DateTime.Now
            };

            var created = await _medicalRecordRepository.AddRecordAsync(mr);
            return new MedicalRecordDto
            {
                Id = created.Id,
                PatientId = created.PatientId,
                Diagnosis = created.Diagnosis,
                Symptoms = created.Symptoms,
                TreatmentPlan = created.TreatmentPlan,
                Notes = created.Notes,
                RecordDate = created.RecordDate
            };
        }

        public async Task<IEnumerable<PatientAllergyDto>> GetPatientAllergiesAsync(int patientId)
        {
            var allergies = await _medicalRecordRepository.GetAllergiesByPatientIdAsync(patientId);
            return allergies.Select(pa => new PatientAllergyDto
            {
                Id = pa.Id,
                PatientId = pa.PatientId,
                AllergyName = pa.AllergyName,
                Severity = pa.Severity,
                Notes = pa.Notes
            });
        }

        public async Task<PatientAllergyDto> AddPatientAllergyAsync(PatientAllergyDto allergyDto)
        {
            var pa = new PatientAllergy
            {
                PatientId = allergyDto.PatientId,
                AllergyName = allergyDto.AllergyName,
                Severity = allergyDto.Severity,
                Notes = allergyDto.Notes
            };

            var created = await _medicalRecordRepository.AddAllergyAsync(pa);
            return new PatientAllergyDto
            {
                Id = created.Id,
                PatientId = created.PatientId,
                AllergyName = created.AllergyName,
                Severity = created.Severity,
                Notes = created.Notes
            };
        }

        public async Task DeletePatientAllergyAsync(int id)
        {
            await _medicalRecordRepository.DeleteAllergyAsync(id);
        }

        public async Task<IEnumerable<VaccinationRecordDto>> GetVaccinationRecordsAsync(int patientId)
        {
            var records = await _medicalRecordRepository.GetVaccinationsByPatientIdAsync(patientId);
            return records.Select(vr => new VaccinationRecordDto
            {
                Id = vr.Id,
                PatientId = vr.PatientId,
                VaccineName = vr.VaccineName,
                VaccinationDate = vr.VaccinationDate,
                DoseNumber = vr.DoseNumber
            });
        }

        public async Task<VaccinationRecordDto> AddVaccinationRecordAsync(VaccinationRecordDto vaccinationDto)
        {
            var vr = new VaccinationRecord
            {
                PatientId = vaccinationDto.PatientId,
                VaccineName = vaccinationDto.VaccineName,
                VaccinationDate = vaccinationDto.VaccinationDate,
                DoseNumber = vaccinationDto.DoseNumber
            };

            var created = await _medicalRecordRepository.AddVaccinationAsync(vr);
            return new VaccinationRecordDto
            {
                Id = created.Id,
                PatientId = created.PatientId,
                VaccineName = created.VaccineName,
                VaccinationDate = created.VaccinationDate,
                DoseNumber = created.DoseNumber
            };
        }

        public async Task DeleteVaccinationRecordAsync(int id)
        {
            await _medicalRecordRepository.DeleteVaccinationAsync(id);
        }
    }
}
