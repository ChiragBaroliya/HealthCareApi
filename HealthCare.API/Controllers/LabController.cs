using HealthCare.Application.DTOs;
using HealthCare.Application.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare.API.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LabController : ControllerBase
    {
        private readonly ILabService _labService;

        public LabController(ILabService labService)
        {
            _labService = labService;
        }

        [HttpGet("tests")]
        public async Task<ActionResult<IEnumerable<LabTestDto>>> GetAllTests()
        {
            var tests = await _labService.GetAllTestsAsync();
            return Ok(tests);
        }

        [HttpPost("tests")]
        public async Task<ActionResult<LabTestDto>> CreateTest(CreateLabTestDto testDto)
        {
            var test = await _labService.CreateTestAsync(testDto);
            return Ok(test);
        }

        [HttpGet("orders/patient/{patientId}")]
        public async Task<ActionResult<IEnumerable<LabOrderDto>>> GetPatientOrders(int patientId)
        {
            var orders = await _labService.GetPatientOrdersAsync(patientId);
            return Ok(orders);
        }

        [HttpPost("orders")]
        public async Task<ActionResult<LabOrderDto>> CreateOrder(CreateLabOrderDto orderDto)
        {
            var order = await _labService.CreateOrderAsync(orderDto);
            return Ok(order);
        }

        [HttpPost("results")]
        public async Task<ActionResult<LabResultDto>> AddResult(CreateLabResultDto resultDto)
        {
            var result = await _labService.AddResultAsync(resultDto);
            return Ok(result);
        }
    }
}
