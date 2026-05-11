using HealthCare.Application.DTOs;
using HealthCare.Application.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare.API.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public MedicalRecordsController(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<IEnumerable<MedicalRecordDto>>> GetRecords(int patientId)
        {
            var records = await _medicalRecordService.GetMedicalRecordsAsync(patientId);
            return Ok(records);
        }

        [HttpPost]
        public async Task<ActionResult<MedicalRecordDto>> CreateRecord(CreateMedicalRecordDto recordDto)
        {
            var record = await _medicalRecordService.CreateMedicalRecordAsync(recordDto);
            return Ok(record);
        }

        [HttpGet("patient/{patientId}/allergies")]
        public async Task<ActionResult<IEnumerable<PatientAllergyDto>>> GetAllergies(int patientId)
        {
            var allergies = await _medicalRecordService.GetPatientAllergiesAsync(patientId);
            return Ok(allergies);
        }

        [HttpPost("allergies")]
        public async Task<ActionResult<PatientAllergyDto>> AddAllergy(PatientAllergyDto allergyDto)
        {
            var allergy = await _medicalRecordService.AddPatientAllergyAsync(allergyDto);
            return Ok(allergy);
        }

        [HttpDelete("allergies/{id}")]
        public async Task<IActionResult> DeleteAllergy(int id)
        {
            await _medicalRecordService.DeletePatientAllergyAsync(id);
            return NoContent();
        }

        [HttpGet("patient/{patientId}/vaccinations")]
        public async Task<ActionResult<IEnumerable<VaccinationRecordDto>>> GetVaccinations(int patientId)
        {
            var records = await _medicalRecordService.GetVaccinationRecordsAsync(patientId);
            return Ok(records);
        }

        [HttpPost("vaccinations")]
        public async Task<ActionResult<VaccinationRecordDto>> AddVaccination(VaccinationRecordDto vaccinationDto)
        {
            var record = await _medicalRecordService.AddVaccinationRecordAsync(vaccinationDto);
            return Ok(record);
        }

        [HttpDelete("vaccinations/{id}")]
        public async Task<IActionResult> DeleteVaccination(int id)
        {
            await _medicalRecordService.DeleteVaccinationRecordAsync(id);
            return NoContent();
        }
    }
}
