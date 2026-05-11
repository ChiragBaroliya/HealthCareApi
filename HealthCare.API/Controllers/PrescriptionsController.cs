using HealthCare.Application.DTOs;
using HealthCare.Application.Services;
using HealthCare.Application.Common;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare.API.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionsController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrescriptionDto>>> GetAll()
        {
            var prescriptions = await _prescriptionService.GetAllPrescriptionsAsync();
            return Ok(prescriptions);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<PrescriptionDto>>> GetPaged([FromQuery] QueryParameters queryParams)
        {
            var prescriptions = await _prescriptionService.GetPagedPrescriptionsAsync(queryParams);
            return Ok(prescriptions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PrescriptionDto>> GetById(int id)
        {
            var prescription = await _prescriptionService.GetPrescriptionByIdAsync(id);
            if (prescription == null) return NotFound();
            return Ok(prescription);
        }

        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<IEnumerable<PrescriptionDto>>> GetByPatientId(int patientId)
        {
            var prescriptions = await _prescriptionService.GetPrescriptionsByPatientIdAsync(patientId);
            return Ok(prescriptions);
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<ActionResult<IEnumerable<PrescriptionDto>>> GetByDoctorId(int doctorId)
        {
            var prescriptions = await _prescriptionService.GetPrescriptionsByDoctorIdAsync(doctorId);
            return Ok(prescriptions);
        }

        [HttpPost]
        public async Task<ActionResult<PrescriptionDto>> Create(CreatePrescriptionDto prescriptionDto)
        {
            var prescription = await _prescriptionService.CreatePrescriptionAsync(prescriptionDto);
            return CreatedAtAction(nameof(GetById), new { id = prescription.Id }, prescription);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _prescriptionService.DeletePrescriptionAsync(id);
            return NoContent();
        }
    }
}
