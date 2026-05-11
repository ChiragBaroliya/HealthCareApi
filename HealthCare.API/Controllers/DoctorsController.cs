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
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetAll()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            return Ok(doctors);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<DoctorDto>>> GetPaged([FromQuery] QueryParameters queryParams)
        {
            var doctors = await _doctorService.GetPagedDoctorsAsync(queryParams);
            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDto>> GetById(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null) return NotFound();
            return Ok(doctor);
        }

        [HttpPost]
        public async Task<ActionResult<DoctorDto>> Create(CreateDoctorDto doctorDto)
        {
            var doctor = await _doctorService.CreateDoctorAsync(doctorDto);
            return CreatedAtAction(nameof(GetById), new { id = doctor.Id }, doctor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateDoctorDto doctorDto)
        {
            try
            {
                await _doctorService.UpdateDoctorAsync(id, doctorDto);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _doctorService.DeleteDoctorAsync(id);
            return NoContent();
        }

        [HttpGet("specializations")]
        public async Task<ActionResult<IEnumerable<DoctorSpecializationDto>>> GetSpecializations()
        {
            var specs = await _doctorService.GetSpecializationsAsync();
            return Ok(specs);
        }

        [HttpPost("specializations")]
        public async Task<ActionResult<DoctorSpecializationDto>> CreateSpecialization(CreateSpecializationDto specDto)
        {
            var spec = await _doctorService.CreateSpecializationAsync(specDto);
            return Ok(spec);
        }

        [HttpPost("{id}/schedule")]
        public async Task<IActionResult> UpdateSchedule(int id, IEnumerable<DoctorScheduleDto> scheduleDtos)
        {
            await _doctorService.UpdateDoctorScheduleAsync(id, scheduleDtos);
            return NoContent();
        }
    }
}
