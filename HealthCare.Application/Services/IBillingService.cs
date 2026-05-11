using System.Collections.Generic;
using System.Threading.Tasks;
using HealthCare.Application.DTOs;
using HealthCare.Application.Common;

namespace HealthCare.Application.Services
{
    public interface IBillingService
    {
        Task<IEnumerable<InvoiceDto>> GetAllInvoicesAsync();
        Task<PagedResult<InvoiceDto>> GetPagedInvoicesAsync(QueryParameters queryParams);
        Task<InvoiceDto> GetInvoiceByIdAsync(int id);
        Task<IEnumerable<InvoiceDto>> GetInvoicesByPatientIdAsync(int patientId);
        Task<InvoiceDto> CreateInvoiceAsync(CreateInvoiceDto invoiceDto);
        
        Task<PaymentDto> ProcessPaymentAsync(CreatePaymentDto paymentDto);
        Task<IEnumerable<PaymentDto>> GetInvoicePaymentsAsync(int invoiceId);
    }
}
