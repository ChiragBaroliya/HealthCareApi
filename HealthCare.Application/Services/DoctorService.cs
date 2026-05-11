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
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync()
        {
            var doctors = await _doctorRepository.GetAllAsync();
            return doctors.Select(MapToDto);
        }

        public async Task<PagedResult<DoctorDto>> GetPagedDoctorsAsync(QueryParameters queryParams)
        {
            var pagedDoctors = await _doctorRepository.GetPagedAsync(queryParams);
            var dtos = pagedDoctors.Items.Select(MapToDto);
            return new PagedResult<DoctorDto>(dtos, pagedDoctors.TotalCount, pagedDoctors.PageNumber, pagedDoctors.PageSize);
        }

        public async Task<DoctorDto> GetDoctorByIdAsync(int id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);
            if (doctor == null) return null;
            return MapToDto(doctor);
        }

        public async Task<DoctorDto> CreateDoctorAsync(CreateDoctorDto doctorDto)
        {
            var doctor = new Doctor
            {
                FullName = doctorDto.FullName,
                SpecializationId = doctorDto.SpecializationId,
                Qualification = doctorDto.Qualification,
                ExperienceYears = doctorDto.ExperienceYears,
                Email = doctorDto.Email,
                PhoneNumber = doctorDto.PhoneNumber,
                ConsultationFee = doctorDto.ConsultationFee,
                LicenseNumber = doctorDto.LicenseNumber,
                AvailabilityStatus = true,
                DoctorCode = "DOC-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper()
            };

            var createdDoctor = await _doctorRepository.AddAsync(doctor);
            return MapToDto(createdDoctor);
        }

        public async Task UpdateDoctorAsync(int id, CreateDoctorDto doctorDto)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);
            if (doctor == null) throw new Exception("Doctor not found");

            doctor.FullName = doctorDto.FullName;
            doctor.SpecializationId = doctorDto.SpecializationId;
            doctor.Qualification = doctorDto.Qualification;
            doctor.ExperienceYears = doctorDto.ExperienceYears;
            doctor.Email = doctorDto.Email;
            doctor.PhoneNumber = doctorDto.PhoneNumber;
            doctor.ConsultationFee = doctorDto.ConsultationFee;
            doctor.LicenseNumber = doctorDto.LicenseNumber;

            await _doctorRepository.UpdateAsync(doctor);
        }

        public async Task DeleteDoctorAsync(int id)
        {
            await _doctorRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<DoctorSpecializationDto>> GetSpecializationsAsync()
        {
            var specs = await _doctorRepository.GetSpecializationsAsync();
            return specs.Select(s => new DoctorSpecializationDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description
            });
        }

        public async Task<DoctorSpecializationDto> CreateSpecializationAsync(CreateSpecializationDto specDto)
        {
            var spec = new DoctorSpecialization
            {
                Name = specDto.Name,
                Description = specDto.Description
            };

            var createdSpec = await _doctorRepository.AddSpecializationAsync(spec);
            return new DoctorSpecializationDto
            {
                Id = createdSpec.Id,
                Name = createdSpec.Name,
                Description = createdSpec.Description
            };
        }

        public async Task UpdateDoctorScheduleAsync(int doctorId, IEnumerable<DoctorScheduleDto> scheduleDtos)
        {
            var schedules = scheduleDtos.Select(s => new DoctorSchedule
            {
                DoctorId = doctorId,
                DayOfWeek = s.DayOfWeek,
                StartTime = TimeSpan.Parse(s.StartTime),
                EndTime = TimeSpan.Parse(s.EndTime),
                IsAvailable = s.IsAvailable
            });

            await _doctorRepository.UpdateSchedulesAsync(doctorId, schedules);
        }

        private DoctorDto MapToDto(Doctor doctor)
        {
            return new DoctorDto
            {
                Id = doctor.Id,
                DoctorCode = doctor.DoctorCode,
                FullName = doctor.FullName,
                SpecializationId = doctor.SpecializationId,
                SpecializationName = doctor.Specialization?.Name,
                Qualification = doctor.Qualification,
                ExperienceYears = doctor.ExperienceYears,
                Email = doctor.Email,
                PhoneNumber = doctor.PhoneNumber,
                ConsultationFee = doctor.ConsultationFee,
                LicenseNumber = doctor.LicenseNumber,
                AvailabilityStatus = doctor.AvailabilityStatus,
                Schedules = doctor.Schedules?.Select(s => new DoctorScheduleDto
                {
                    Id = s.Id,
                    DoctorId = s.DoctorId,
                    DayOfWeek = s.DayOfWeek,
                    StartTime = s.StartTime.ToString(),
                    EndTime = s.EndTime.ToString(),
                    IsAvailable = s.IsAvailable
                }).ToList() ?? new List<DoctorScheduleDto>()
            };
        }
    }
}
