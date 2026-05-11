using System.Collections.Generic;
using HealthCare.Domain.Enums;

namespace HealthCare.Domain.Entities
{
    public class Invoice : BaseEntity
    {
        public string InvoiceNumber { get; set; }
        public int PatientId { get; set; }
        public int? AppointmentId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public InvoiceStatus Status { get; set; }

        // Navigation properties
        public Patient Patient { get; set; }
        public Appointment Appointment { get; set; }
        public ICollection<Payment> Payments { get; set; }

        public Invoice()
        {
            Payments = new HashSet<Payment>();
        }
    }
}
