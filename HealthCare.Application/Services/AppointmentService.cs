using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCare.Application.DTOs;
using HealthCare.Application.Repositories;
using HealthCare.Application.Common;
using HealthCare.Domain.Entities;
using HealthCare.Domain.Enums;

namespace HealthCare.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync()
        {
            var appointments = await _appointmentRepository.GetAllAsync();
            return appointments.Select(MapToDto);
        }

        public async Task<PagedResult<AppointmentDto>> GetPagedAppointmentsAsync(QueryParameters queryParams)
        {
            var pagedAppointments = await _appointmentRepository.GetPagedAsync(queryParams);
            var dtos = pagedAppointments.Items.Select(MapToDto);
            return new PagedResult<AppointmentDto>(dtos, pagedAppointments.TotalCount, pagedAppointments.PageNumber, pagedAppointments.PageSize);
        }

        public async Task<AppointmentDto> GetAppointmentByIdAsync(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment == null) return null;
            return MapToDto(appointment);
        }

        public async Task<IEnumerable<AppointmentDto>> GetAppointmentsByPatientIdAsync(int patientId)
        {
            var appointments = await _appointmentRepository.GetByPatientIdAsync(patientId);
            return appointments.Select(MapToDto);
        }

        public async Task<IEnumerable<AppointmentDto>> GetAppointmentsByDoctorIdAsync(int doctorId)
        {
            var appointments = await _appointmentRepository.GetByDoctorIdAsync(doctorId);
            return appointments.Select(MapToDto);
        }

        public async Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto appointmentDto)
        {
            var appointment = new Appointment
            {
                PatientId = appointmentDto.PatientId,
                DoctorId = appointmentDto.DoctorId,
                AppointmentDate = appointmentDto.AppointmentDate,
                StartTime = TimeSpan.Parse(appointmentDto.StartTime),
                EndTime = TimeSpan.Parse(appointmentDto.EndTime),
                Reason = appointmentDto.Reason,
                Notes = appointmentDto.Notes,
                Status = AppointmentStatus.Pending,
                AppointmentNumber = "APT-" + DateTime.Now.ToString("yyyyMMdd") + "-" + Guid.NewGuid().ToString().Substring(0, 4).ToUpper()
            };

            var createdAppointment = await _appointmentRepository.AddAsync(appointment);
            return MapToDto(createdAppointment);
        }

        public async Task UpdateAppointmentAsync(int id, CreateAppointmentDto appointmentDto)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment == null) throw new Exception("Appointment not found");

            appointment.PatientId = appointmentDto.PatientId;
            appointment.DoctorId = appointmentDto.DoctorId;
            appointment.AppointmentDate = appointmentDto.AppointmentDate;
            appointment.StartTime = TimeSpan.Parse(appointmentDto.StartTime);
            appointment.EndTime = TimeSpan.Parse(appointmentDto.EndTime);
            appointment.Reason = appointmentDto.Reason;
            appointment.Notes = appointmentDto.Notes;

            await _appointmentRepository.UpdateAsync(appointment);
        }

        public async Task UpdateAppointmentStatusAsync(int id, AppointmentStatus status)
        {
            await _appointmentRepository.UpdateStatusAsync(id, status);
        }

        public async Task DeleteAppointmentAsync(int id)
        {
            await _appointmentRepository.DeleteAsync(id);
        }

        private AppointmentDto MapToDto(Appointment appointment)
        {
            return new AppointmentDto
            {
                Id = appointment.Id,
                AppointmentNumber = appointment.AppointmentNumber,
                PatientId = appointment.PatientId,
                PatientName = appointment.Patient?.FirstName + " " + appointment.Patient?.LastName,
                DoctorId = appointment.DoctorId,
                DoctorName = appointment.Doctor?.FullName,
                AppointmentDate = appointment.AppointmentDate,
                StartTime = appointment.StartTime.ToString(),
                EndTime = appointment.EndTime.ToString(),
                Status = appointment.Status,
                Reason = appointment.Reason,
                Notes = appointment.Notes
            };
        }
    }
}
