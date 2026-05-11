using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCare.Application.DTOs;
using HealthCare.Application.Repositories;
using HealthCare.Application.Common;
using HealthCare.Domain.Entities;

namespace HealthCare.Application.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly IMedicineRepository _medicineRepository;

        public PrescriptionService(IPrescriptionRepository prescriptionRepository, IMedicineRepository medicineRepository)
        {
            _prescriptionRepository = prescriptionRepository;
            _medicineRepository = medicineRepository;
        }

        public async Task<IEnumerable<PrescriptionDto>> GetAllPrescriptionsAsync()
        {
            var prescriptions = await _prescriptionRepository.GetAllAsync();
            return prescriptions.Select(MapToDto);
        }

        public async Task<PagedResult<PrescriptionDto>> GetPagedPrescriptionsAsync(QueryParameters queryParams)
        {
            var paged = await _prescriptionRepository.GetPagedAsync(queryParams);
            var dtos = paged.Items.Select(MapToDto);
            return new PagedResult<PrescriptionDto>(dtos, paged.TotalCount, paged.PageNumber, paged.PageSize);
        }

        public async Task<PrescriptionDto> GetPrescriptionByIdAsync(int id)
        {
            var prescription = await _prescriptionRepository.GetByIdAsync(id);
            if (prescription == null) return null;
            return MapToDto(prescription);
        }

        public async Task<IEnumerable<PrescriptionDto>> GetPrescriptionsByPatientIdAsync(int patientId)
        {
            var prescriptions = await _prescriptionRepository.GetByPatientIdAsync(patientId);
            return prescriptions.Select(MapToDto);
        }

        public async Task<IEnumerable<PrescriptionDto>> GetPrescriptionsByDoctorIdAsync(int doctorId)
        {
            var prescriptions = await _prescriptionRepository.GetByDoctorIdAsync(doctorId);
            return prescriptions.Select(MapToDto);
        }

        public async Task<PrescriptionDto> CreatePrescriptionAsync(CreatePrescriptionDto prescriptionDto)
        {
            var prescription = new Prescription
            {
                AppointmentId = prescriptionDto.AppointmentId,
                DoctorId = prescriptionDto.DoctorId,
                PatientId = prescriptionDto.PatientId,
                PrescriptionDate = DateTime.Now,
                Notes = prescriptionDto.Notes,
                FollowUpDate = prescriptionDto.FollowUpDate,
                PrescriptionMedicines = prescriptionDto.Medicines.Select(m => new PrescriptionMedicine
                {
                    MedicineId = m.MedicineId,
                    Dosage = m.Dosage,
                    Frequency = m.Frequency,
                    Duration = m.Duration,
                    Instructions = m.Instructions
                }).ToList()
            };

            var createdPrescription = await _prescriptionRepository.AddAsync(prescription);
            return await GetPrescriptionByIdAsync(createdPrescription.Id);
        }

        public async Task DeletePrescriptionAsync(int id)
        {
            await _prescriptionRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<MedicineDto>> GetAllMedicinesAsync()
        {
            var medicines = await _medicineRepository.GetAllAsync();
            return medicines.Select(m => new MedicineDto
            {
                Id = m.Id,
                MedicineName = m.MedicineName,
                Manufacturer = m.Manufacturer,
                MedicineType = m.MedicineType,
                UnitPrice = m.UnitPrice,
                StockQuantity = m.StockQuantity
            });
        }

        public async Task<PagedResult<MedicineDto>> GetPagedMedicinesAsync(QueryParameters queryParams)
        {
            var paged = await _medicineRepository.GetPagedAsync(queryParams);
            var dtos = paged.Items.Select(m => new MedicineDto
            {
                Id = m.Id,
                MedicineName = m.MedicineName,
                Manufacturer = m.Manufacturer,
                MedicineType = m.MedicineType,
                UnitPrice = m.UnitPrice,
                StockQuantity = m.StockQuantity
            });
            return new PagedResult<MedicineDto>(dtos, paged.TotalCount, paged.PageNumber, paged.PageSize);
        }

        public async Task<MedicineDto> GetMedicineByIdAsync(int id)
        {
            var m = await _medicineRepository.GetByIdAsync(id);
            if (m == null) return null;
            return new MedicineDto
            {
                Id = m.Id,
                MedicineName = m.MedicineName,
                Manufacturer = m.Manufacturer,
                MedicineType = m.MedicineType,
                UnitPrice = m.UnitPrice,
                StockQuantity = m.StockQuantity
            };
        }

        public async Task<MedicineDto> CreateMedicineAsync(CreateMedicineDto medicineDto)
        {
            var m = new Medicine
            {
                MedicineName = medicineDto.MedicineName,
                Manufacturer = medicineDto.Manufacturer,
                MedicineType = medicineDto.MedicineType,
                UnitPrice = medicineDto.UnitPrice,
                StockQuantity = medicineDto.StockQuantity
            };

            var created = await _medicineRepository.AddAsync(m);
            return await GetMedicineByIdAsync(created.Id);
        }

        public async Task UpdateMedicineAsync(int id, CreateMedicineDto medicineDto)
        {
            var m = await _medicineRepository.GetByIdAsync(id);
            if (m == null) throw new Exception("Medicine not found");

            m.MedicineName = medicineDto.MedicineName;
            m.Manufacturer = medicineDto.Manufacturer;
            m.MedicineType = medicineDto.MedicineType;
            m.UnitPrice = medicineDto.UnitPrice;
            m.StockQuantity = medicineDto.StockQuantity;

            await _medicineRepository.UpdateAsync(m);
        }

        public async Task DeleteMedicineAsync(int id)
        {
            await _medicineRepository.DeleteAsync(id);
        }

        private PrescriptionDto MapToDto(Prescription p)
        {
            return new PrescriptionDto
            {
                Id = p.Id,
                AppointmentId = p.AppointmentId,
                AppointmentNumber = p.Appointment?.AppointmentNumber,
                DoctorId = p.DoctorId,
                DoctorName = p.Doctor?.FullName,
                PatientId = p.PatientId,
                PatientName = p.Patient?.FirstName + " " + p.Patient?.LastName,
                PrescriptionDate = p.PrescriptionDate,
                Notes = p.Notes,
                FollowUpDate = p.FollowUpDate,
                Medicines = p.PrescriptionMedicines?.Select(pm => new PrescriptionMedicineDto
                {
                    Id = pm.Id,
                    MedicineId = pm.MedicineId,
                    MedicineName = pm.Medicine?.MedicineName,
                    Dosage = pm.Dosage,
                    Frequency = pm.Frequency,
                    Duration = pm.Duration,
                    Instructions = pm.Instructions
                }).ToList() ?? new List<PrescriptionMedicineDto>()
            };
        }
    }
}
