namespace HealthCare.Domain.Entities
{
    public class Medicine : BaseEntity
    {
        public string MedicineName { get; set; }
        public string Manufacturer { get; set; }
        public string MedicineType { get; set; }
        public decimal UnitPrice { get; set; }
        public int StockQuantity { get; set; }
    }
}
