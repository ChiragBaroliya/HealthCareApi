using System;

namespace HealthCare.Domain.Entities
{
    public class PatientAddress : BaseEntity
    {
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}
