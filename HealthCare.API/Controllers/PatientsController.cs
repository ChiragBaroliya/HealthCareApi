using Microsoft.AspNetCore.Mvc;
using HealthCare.Application.Services;
using HealthCare.Application.DTOs;
using HealthCare.Application.Models;
using HealthCare.Application.Common;
using Asp.Versioning;

namespace HealthCare.API.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _service;

        public PatientsController(IPatientService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _service.GetAllAsync();
            if (!res.Success) return BadRequest(res);
            return Ok(res);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] QueryParameters queryParams)
        {
            var res = await _service.GetPagedAsync(queryParams);
            if (!res.Success) return BadRequest(res);
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var res = await _service.GetByIdAsync(id);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PatientDto dto)
        {
            var res = await _service.CreateAsync(dto);
            if (!res.Success) return BadRequest(res);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PatientDto dto)
        {
            var res = await _service.UpdateAsync(id, dto);
            if (!res.Success) return BadRequest(res);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _service.DeleteAsync(id);
            if (!res.Success) return BadRequest(res);
            return Ok(res);
        }
    }
}
