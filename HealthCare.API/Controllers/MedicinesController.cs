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
    public class MedicinesController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;

        public MedicinesController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicineDto>>> GetAll()
        {
            var medicines = await _prescriptionService.GetAllMedicinesAsync();
            return Ok(medicines);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<MedicineDto>>> GetPaged([FromQuery] QueryParameters queryParams)
        {
            var medicines = await _prescriptionService.GetPagedMedicinesAsync(queryParams);
            return Ok(medicines);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicineDto>> GetById(int id)
        {
            var medicine = await _prescriptionService.GetMedicineByIdAsync(id);
            if (medicine == null) return NotFound();
            return Ok(medicine);
        }

        [HttpPost]
        public async Task<ActionResult<MedicineDto>> Create(CreateMedicineDto medicineDto)
        {
            var medicine = await _prescriptionService.CreateMedicineAsync(medicineDto);
            return CreatedAtAction(nameof(GetById), new { id = medicine.Id }, medicine);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateMedicineDto medicineDto)
        {
            try
            {
                await _prescriptionService.UpdateMedicineAsync(id, medicineDto);
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
            await _prescriptionService.DeleteMedicineAsync(id);
            return NoContent();
        }
    }
}
