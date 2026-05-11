namespace HealthCare.Application.DTOs
{
    public class EmergencyContactDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string FullName { get; set; }
        public string Relationship { get; set; }
        public string ContactNumber { get; set; }
    }
}
