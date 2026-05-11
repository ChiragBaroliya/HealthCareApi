using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCare.Application.Repositories;
using HealthCare.Domain.Entities;
using HealthCare.Infrastructure.Data;
using HealthCare.Application.Common;
using HealthCare.Application.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Infrastructure.Repositories
{
    public class BillingRepository : IBillingRepository
    {
        private readonly HealthCareDbContext _context;

        public BillingRepository(HealthCareDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
        {
            return await _context.Invoices
                .Include(i => i.Patient)
                .Include(i => i.Appointment)
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();
        }

        public async Task<PagedResult<Invoice>> GetPagedInvoicesAsync(QueryParameters queryParams)
        {
            var query = _context.Invoices
                .Include(i => i.Patient)
                .Include(i => i.Appointment)
                .AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(queryParams.SearchTerm))
            {
                query = query.Where(i => 
                    i.InvoiceNumber.Contains(queryParams.SearchTerm) || 
                    i.Patient.FirstName.Contains(queryParams.SearchTerm) || 
                    i.Patient.LastName.Contains(queryParams.SearchTerm));
            }

            // Sorting
            query = query.ApplySorting(queryParams.SortBy, queryParams.IsDescending);

            var totalCount = await query.CountAsync();

            // Pagination
            var invoices = await query
                .ApplyPagination(queryParams.PageNumber, queryParams.PageSize)
                .ToListAsync();

            return new PagedResult<Invoice>(invoices, totalCount, queryParams.PageNumber, queryParams.PageSize);
        }

        public async Task<Invoice> GetInvoiceByIdAsync(int id)
        {
            return await _context.Invoices
                .Include(i => i.Patient)
                .Include(i => i.Appointment)
                .Include(i => i.Payments)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Invoice>> GetInvoicesByPatientIdAsync(int patientId)
        {
            return await _context.Invoices
                .Include(i => i.Appointment)
                .Where(i => i.PatientId == patientId)
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();
        }

        public async Task<Invoice> AddInvoiceAsync(Invoice invoice)
        {
            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
            return invoice;
        }

        public async Task UpdateInvoiceAsync(Invoice invoice)
        {
            _context.Invoices.Update(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task<Payment> AddPaymentAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByInvoiceIdAsync(int invoiceId)
        {
            return await _context.Payments
                .Where(p => p.InvoiceId == invoiceId)
                .ToListAsync();
        }
    }
}
