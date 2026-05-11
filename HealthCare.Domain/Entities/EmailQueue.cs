namespace HealthCare.Domain.Entities
{
    public class EmailQueue : BaseEntity
    {
        public string RecipientEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Status { get; set; } // e.g., Pending, Sent, Failed
        public int RetryCount { get; set; }
    }
}
