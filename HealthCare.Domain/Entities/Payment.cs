using System;
using HealthCare.Domain.Enums;

namespace HealthCare.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public int InvoiceId { get; set; }
        public string PaymentMethod { get; set; } // e.g., Cash, Card, Online
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        // Navigation property
        public Invoice Invoice { get; set; }
    }
}
