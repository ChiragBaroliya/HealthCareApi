using HealthCare.Application.DTOs;
using HealthCare.Application.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare.API.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class HospitalsController : ControllerBase
    {
        private readonly IHospitalService _hospitalService;

        public HospitalsController(IHospitalService hospitalService)
        {
            _hospitalService = hospitalService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalDto>>> GetAll()
        {
            var hospitals = await _hospitalService.GetAllHospitalsAsync();
            return Ok(hospitals);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalDto>> GetById(int id)
        {
            var hospital = await _hospitalService.GetHospitalByIdAsync(id);
            if (hospital == null) return NotFound();
            return Ok(hospital);
        }

        [HttpPost]
        public async Task<ActionResult<HospitalDto>> Create(CreateHospitalDto hospitalDto)
        {
            var hospital = await _hospitalService.CreateHospitalAsync(hospitalDto);
            return CreatedAtAction(nameof(GetById), new { id = hospital.Id }, hospital);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateHospitalDto hospitalDto)
        {
            try
            {
                await _hospitalService.UpdateHospitalAsync(id, hospitalDto);
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
            await _hospitalService.DeleteHospitalAsync(id);
            return NoContent();
        }

        [HttpGet("{hospitalId}/departments")]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetDepartments(int hospitalId)
        {
            var depts = await _hospitalService.GetDepartmentsAsync(hospitalId);
            return Ok(depts);
        }

        [HttpPost("departments")]
        public async Task<ActionResult<DepartmentDto>> CreateDepartment(CreateDepartmentDto deptDto)
        {
            var dept = await _hospitalService.CreateDepartmentAsync(deptDto);
            return Ok(dept);
        }

        [HttpDelete("departments/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            await _hospitalService.DeleteDepartmentAsync(id);
            return NoContent();
        }
    }
}
