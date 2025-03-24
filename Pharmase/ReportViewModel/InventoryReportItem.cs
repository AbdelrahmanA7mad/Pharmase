namespace Pharmase.ReportViewModel
{
    public class InventoryReportItem
    {
        public string MedicineName { get; set; }
        public string Category { get; set; }
        public int CurrentStock { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
