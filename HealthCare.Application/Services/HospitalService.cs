using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCare.Application.DTOs;
using HealthCare.Application.Repositories;
using HealthCare.Domain.Entities;

namespace HealthCare.Application.Services
{
    public class HospitalService : IHospitalService
    {
        private readonly IHospitalRepository _hospitalRepository;

        public HospitalService(IHospitalRepository hospitalRepository)
        {
            _hospitalRepository = hospitalRepository;
        }

        public async Task<IEnumerable<HospitalDto>> GetAllHospitalsAsync()
        {
            var hospitals = await _hospitalRepository.GetAllAsync();
            return hospitals.Select(MapToDto);
        }

        public async Task<HospitalDto> GetHospitalByIdAsync(int id)
        {
            var hospital = await _hospitalRepository.GetByIdAsync(id);
            if (hospital == null) return null;
            return MapToDto(hospital);
        }

        public async Task<HospitalDto> CreateHospitalAsync(CreateHospitalDto hospitalDto)
        {
            var hospital = new Hospital
            {
                Name = hospitalDto.Name,
                Address = hospitalDto.Address,
                ContactNumber = hospitalDto.ContactNumber,
                Email = hospitalDto.Email
            };

            var created = await _hospitalRepository.AddAsync(hospital);
            return MapToDto(created);
        }

        public async Task UpdateHospitalAsync(int id, CreateHospitalDto hospitalDto)
        {
            var hospital = await _hospitalRepository.GetByIdAsync(id);
            if (hospital == null) throw new System.Exception("Hospital not found");

            hospital.Name = hospitalDto.Name;
            hospital.Address = hospitalDto.Address;
            hospital.ContactNumber = hospitalDto.ContactNumber;
            hospital.Email = hospitalDto.Email;

            await _hospitalRepository.UpdateAsync(hospital);
        }

        public async Task DeleteHospitalAsync(int id)
        {
            await _hospitalRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync(int hospitalId)
        {
            var depts = await _hospitalRepository.GetDepartmentsByHospitalIdAsync(hospitalId);
            return depts.Select(d => new DepartmentDto
            {
                Id = d.Id,
                HospitalId = d.HospitalId,
                DepartmentName = d.DepartmentName,
                Description = d.Description
            });
        }

        public async Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto deptDto)
        {
            var dept = new Department
            {
                HospitalId = deptDto.HospitalId,
                DepartmentName = deptDto.DepartmentName,
                Description = deptDto.Description
            };

            var created = await _hospitalRepository.AddDepartmentAsync(dept);
            return new DepartmentDto
            {
                Id = created.Id,
                HospitalId = created.HospitalId,
                DepartmentName = created.DepartmentName,
                Description = created.Description
            };
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            await _hospitalRepository.DeleteDepartmentAsync(id);
        }

        private HospitalDto MapToDto(Hospital h)
        {
            return new HospitalDto
            {
                Id = h.Id,
                Name = h.Name,
                Address = h.Address,
                ContactNumber = h.ContactNumber,
                Email = h.Email,
                Departments = h.Departments?.Select(d => new DepartmentDto
                {
                    Id = d.Id,
                    HospitalId = d.HospitalId,
                    DepartmentName = d.DepartmentName,
                    Description = d.Description
                }).ToList() ?? new List<DepartmentDto>()
            };
        }
    }
}
