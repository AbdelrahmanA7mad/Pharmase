namespace Pharmase.ReportViewModel
{
    public class DailySales
    {
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public int TransactionCount { get; set; }
        public decimal AverageTransaction => TransactionCount > 0
            ? TotalAmount / TransactionCount
            : 0;
    }
}
