using System;

namespace HealthCare.Domain.Entities
{
    public class PharmacyInventory : BaseEntity
    {
        public int MedicineId { get; set; }
        public int AvailableStock { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string BatchNumber { get; set; }

        // Navigation property
        public Medicine Medicine { get; set; }
    }
}
