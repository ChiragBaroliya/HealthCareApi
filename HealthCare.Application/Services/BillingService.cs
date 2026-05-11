using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCare.Application.DTOs;
using HealthCare.Application.Repositories;
using HealthCare.Application.Common;
using HealthCare.Domain.Entities;
using HealthCare.Domain.Enums;

namespace HealthCare.Application.Services
{
    public class BillingService : IBillingService
    {
        private readonly IBillingRepository _billingRepository;

        public BillingService(IBillingRepository billingRepository)
        {
            _billingRepository = billingRepository;
        }

        public async Task<IEnumerable<InvoiceDto>> GetAllInvoicesAsync()
        {
            var invoices = await _billingRepository.GetAllInvoicesAsync();
            return invoices.Select(MapToDto);
        }

        public async Task<PagedResult<InvoiceDto>> GetPagedInvoicesAsync(QueryParameters queryParams)
        {
            var paged = await _billingRepository.GetPagedInvoicesAsync(queryParams);
            var dtos = paged.Items.Select(MapToDto);
            return new PagedResult<InvoiceDto>(dtos, paged.TotalCount, paged.PageNumber, paged.PageSize);
        }

        public async Task<InvoiceDto> GetInvoiceByIdAsync(int id)
        {
            var invoice = await _billingRepository.GetInvoiceByIdAsync(id);
            if (invoice == null) return null;
            return MapToDto(invoice);
        }

        public async Task<IEnumerable<InvoiceDto>> GetInvoicesByPatientIdAsync(int patientId)
        {
            var invoices = await _billingRepository.GetInvoicesByPatientIdAsync(patientId);
            return invoices.Select(MapToDto);
        }

        public async Task<InvoiceDto> CreateInvoiceAsync(CreateInvoiceDto invoiceDto)
        {
            var invoice = new Invoice
            {
                PatientId = invoiceDto.PatientId,
                AppointmentId = invoiceDto.AppointmentId,
                TotalAmount = invoiceDto.TotalAmount,
                TaxAmount = invoiceDto.TaxAmount,
                DiscountAmount = invoiceDto.DiscountAmount,
                PaidAmount = 0,
                Status = InvoiceStatus.Unpaid,
                InvoiceNumber = "INV-" + DateTime.Now.ToString("yyyyMMdd") + "-" + Guid.NewGuid().ToString().Substring(0, 4).ToUpper()
            };

            var created = await _billingRepository.AddInvoiceAsync(invoice);
            return await GetInvoiceByIdAsync(created.Id);
        }

        public async Task<PaymentDto> ProcessPaymentAsync(CreatePaymentDto paymentDto)
        {
            var invoice = await _billingRepository.GetInvoiceByIdAsync(paymentDto.InvoiceId);
            if (invoice == null) throw new Exception("Invoice not found");

            var payment = new Payment
            {
                InvoiceId = paymentDto.InvoiceId,
                PaymentMethod = paymentDto.PaymentMethod,
                TransactionId = paymentDto.TransactionId,
                Amount = paymentDto.Amount,
                PaymentDate = DateTime.Now,
                PaymentStatus = PaymentStatus.Paid
            };

            var createdPayment = await _billingRepository.AddPaymentAsync(payment);

            // Update Invoice
            invoice.PaidAmount += paymentDto.Amount;
            if (invoice.PaidAmount >= invoice.TotalAmount)
                invoice.Status = InvoiceStatus.Paid;
            else if (invoice.PaidAmount > 0)
                invoice.Status = InvoiceStatus.PartiallyPaid;

            await _billingRepository.UpdateInvoiceAsync(invoice);

            return new PaymentDto
            {
                Id = createdPayment.Id,
                InvoiceId = createdPayment.InvoiceId,
                PaymentMethod = createdPayment.PaymentMethod,
                TransactionId = createdPayment.TransactionId,
                Amount = createdPayment.Amount,
                PaymentDate = createdPayment.PaymentDate,
                PaymentStatus = createdPayment.PaymentStatus
            };
        }

        public async Task<IEnumerable<PaymentDto>> GetInvoicePaymentsAsync(int invoiceId)
        {
            var payments = await _billingRepository.GetPaymentsByInvoiceIdAsync(invoiceId);
            return payments.Select(p => new PaymentDto
            {
                Id = p.Id,
                InvoiceId = p.InvoiceId,
                PaymentMethod = p.PaymentMethod,
                TransactionId = p.TransactionId,
                Amount = p.Amount,
                PaymentDate = p.PaymentDate,
                PaymentStatus = p.PaymentStatus
            });
        }

        private InvoiceDto MapToDto(Invoice i)
        {
            return new InvoiceDto
            {
                Id = i.Id,
                InvoiceNumber = i.InvoiceNumber,
                PatientId = i.PatientId,
                PatientName = i.Patient?.FirstName + " " + i.Patient?.LastName,
                AppointmentId = i.AppointmentId,
                AppointmentNumber = i.Appointment?.AppointmentNumber,
                TotalAmount = i.TotalAmount,
                TaxAmount = i.TaxAmount,
                DiscountAmount = i.DiscountAmount,
                PaidAmount = i.PaidAmount,
                Status = i.Status,
                Payments = i.Payments?.Select(p => new PaymentDto
                {
                    Id = p.Id,
                    InvoiceId = p.InvoiceId,
                    PaymentMethod = p.PaymentMethod,
                    TransactionId = p.TransactionId,
                    Amount = p.Amount,
                    PaymentDate = p.PaymentDate,
                    PaymentStatus = p.PaymentStatus
                }).ToList() ?? new List<PaymentDto>()
            };
        }
    }
}
