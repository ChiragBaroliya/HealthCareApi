using System;

namespace HealthCare.Domain.Entities
{
    public class Document : BaseEntity
    {
        public int PatientId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; } // e.g., X-Ray, MRI, Report, Prescription
        public string BlobUrl { get; set; }
        public DateTime UploadedDate { get; set; }

        // Navigation property
        public Patient Patient { get; set; }
    }
}
