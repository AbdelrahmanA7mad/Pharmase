namespace Pharmase.ReportViewModel
{
    public class SalesReportViewModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal TotalSales { get; set; }
        public int TotalTransactions { get; set; }
        public decimal AverageSale { get; set; }
        public List<DailySales> DailySales { get; set; } = new List<DailySales>();
        public decimal HighestDailySale { get; set; }
        public decimal LowestDailySale { get; set; }
    }
}
