using System.Collections.Generic;
using System.Threading.Tasks;
using HealthCare.Domain.Entities;
using HealthCare.Application.Common;

namespace HealthCare.Application.Repositories
{
    public interface IBillingRepository
    {
        Task<IEnumerable<Invoice>> GetAllInvoicesAsync();
        Task<PagedResult<Invoice>> GetPagedInvoicesAsync(QueryParameters queryParams);
        Task<Invoice> GetInvoiceByIdAsync(int id);
        Task<IEnumerable<Invoice>> GetInvoicesByPatientIdAsync(int patientId);
        Task<Invoice> AddInvoiceAsync(Invoice invoice);
        Task UpdateInvoiceAsync(Invoice invoice);
        
        Task<Payment> AddPaymentAsync(Payment payment);
        Task<IEnumerable<Payment>> GetPaymentsByInvoiceIdAsync(int invoiceId);
    }
}
