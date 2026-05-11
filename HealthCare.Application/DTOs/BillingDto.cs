using System;
using System.Collections.Generic;
using HealthCare.Domain.Enums;

namespace HealthCare.Application.DTOs
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int? AppointmentId { get; set; }
        public string AppointmentNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount => TotalAmount - PaidAmount;
        public InvoiceStatus Status { get; set; }
        public List<PaymentDto> Payments { get; set; } = new();
    }

    public class CreateInvoiceDto
    {
        public int PatientId { get; set; }
        public int? AppointmentId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
    }

    public class PaymentDto
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string PaymentMethod { get; set; }
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }

    public class CreatePaymentDto
    {
        public int InvoiceId { get; set; }
        public string PaymentMethod { get; set; }
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
    }
}
