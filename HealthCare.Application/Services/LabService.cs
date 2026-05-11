using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCare.Application.DTOs;
using HealthCare.Application.Repositories;
using HealthCare.Domain.Entities;

namespace HealthCare.Application.Services
{
    public class LabService : ILabService
    {
        private readonly ILabRepository _labRepository;

        public LabService(ILabRepository labRepository)
        {
            _labRepository = labRepository;
        }

        public async Task<IEnumerable<LabTestDto>> GetAllTestsAsync()
        {
            var tests = await _labRepository.GetAllTestsAsync();
            return tests.Select(t => new LabTestDto
            {
                Id = t.Id,
                TestName = t.TestName,
                Description = t.Description,
                Cost = t.Cost
            });
        }

        public async Task<LabTestDto> CreateTestAsync(CreateLabTestDto testDto)
        {
            var test = new LabTest
            {
                TestName = testDto.TestName,
                Description = testDto.Description,
                Cost = testDto.Cost
            };

            var created = await _labRepository.AddTestAsync(test);
            return new LabTestDto
            {
                Id = created.Id,
                TestName = created.TestName,
                Description = created.Description,
                Cost = created.Cost
            };
        }

        public async Task<IEnumerable<LabOrderDto>> GetPatientOrdersAsync(int patientId)
        {
            var orders = await _labRepository.GetOrdersByPatientIdAsync(patientId);
            return orders.Select(MapToOrderDto);
        }

        public async Task<LabOrderDto> CreateOrderAsync(CreateLabOrderDto orderDto)
        {
            var order = new LabOrder
            {
                PatientId = orderDto.PatientId,
                DoctorId = orderDto.DoctorId,
                AppointmentId = orderDto.AppointmentId,
                OrderDate = DateTime.Now,
                Status = "Pending"
            };

            var created = await _labRepository.AddOrderAsync(order);
            return MapToOrderDto(created);
        }

        public async Task<LabResultDto> AddResultAsync(CreateLabResultDto resultDto)
        {
            var result = new LabResult
            {
                LabOrderId = resultDto.LabOrderId,
                ResultValue = resultDto.ResultValue,
                NormalRange = resultDto.NormalRange,
                Remarks = resultDto.Remarks
            };

            var created = await _labRepository.AddResultAsync(result);

            // Update order status if needed
            var order = await _labRepository.GetOrderByIdAsync(resultDto.LabOrderId);
            if (order != null && order.Status == "Pending")
            {
                order.Status = "Completed";
                await _labRepository.UpdateOrderAsync(order);
            }

            return new LabResultDto
            {
                Id = created.Id,
                LabOrderId = created.LabOrderId,
                ResultValue = created.ResultValue,
                NormalRange = created.NormalRange,
                Remarks = created.Remarks
            };
        }

        private LabOrderDto MapToOrderDto(LabOrder lo)
        {
            return new LabOrderDto
            {
                Id = lo.Id,
                PatientId = lo.PatientId,
                PatientName = lo.Patient?.FirstName + " " + lo.Patient?.LastName,
                DoctorId = lo.DoctorId,
                DoctorName = lo.Doctor?.FullName,
                AppointmentId = lo.AppointmentId,
                OrderDate = lo.OrderDate,
                Status = lo.Status,
                Results = lo.LabResults?.Select(r => new LabResultDto
                {
                    Id = r.Id,
                    LabOrderId = r.LabOrderId,
                    ResultValue = r.ResultValue,
                    NormalRange = r.NormalRange,
                    Remarks = r.Remarks
                }).ToList() ?? new List<LabResultDto>()
            };
        }
    }
}
