using System;
using System.Collections.Generic;

namespace HealthCare.Domain.Entities
{
    public class Patient : BaseEntity
    {
        public string PatientCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string BloodGroup { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string EmergencyContact { get; set; }
        public string MaritalStatus { get; set; }
        public string Nationality { get; set; }
        public string MedicalHistory { get; set; }
        public string Allergies { get; set; }
        public string InsuranceProvider { get; set; }
        public string InsuranceNumber { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<PatientAddress> PatientAddresses { get; set; } = new List<PatientAddress>();
        public ICollection<EmergencyContact> EmergencyContacts { get; set; } = new List<EmergencyContact>();
    }
}
