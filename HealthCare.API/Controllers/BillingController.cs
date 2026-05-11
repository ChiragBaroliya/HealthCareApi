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
    public class BillingController : ControllerBase
    {
        private readonly IBillingService _billingService;

        public BillingController(IBillingService billingService)
        {
            _billingService = billingService;
        }

        [HttpGet("invoices")]
        public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetAllInvoices()
        {
            var invoices = await _billingService.GetAllInvoicesAsync();
            return Ok(invoices);
        }

        [HttpGet("invoices/paged")]
        public async Task<ActionResult<PagedResult<InvoiceDto>>> GetPagedInvoices([FromQuery] QueryParameters queryParams)
        {
            var invoices = await _billingService.GetPagedInvoicesAsync(queryParams);
            return Ok(invoices);
        }

        [HttpGet("invoices/{id}")]
        public async Task<ActionResult<InvoiceDto>> GetInvoiceById(int id)
        {
            var invoice = await _billingService.GetInvoiceByIdAsync(id);
            if (invoice == null) return NotFound();
            return Ok(invoice);
        }

        [HttpGet("invoices/patient/{patientId}")]
        public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetInvoicesByPatient(int patientId)
        {
            var invoices = await _billingService.GetInvoicesByPatientIdAsync(patientId);
            return Ok(invoices);
        }

        [HttpPost("invoices")]
        public async Task<ActionResult<InvoiceDto>> CreateInvoice(CreateInvoiceDto invoiceDto)
        {
            var invoice = await _billingService.CreateInvoiceAsync(invoiceDto);
            return CreatedAtAction(nameof(GetInvoiceById), new { id = invoice.Id }, invoice);
        }

        [HttpPost("payments")]
        public async Task<ActionResult<PaymentDto>> ProcessPayment(CreatePaymentDto paymentDto)
        {
            try
            {
                var payment = await _billingService.ProcessPaymentAsync(paymentDto);
                return Ok(payment);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("invoices/{invoiceId}/payments")]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetInvoicePayments(int invoiceId)
        {
            var payments = await _billingService.GetInvoicePaymentsAsync(invoiceId);
            return Ok(payments);
        }
    }
}
