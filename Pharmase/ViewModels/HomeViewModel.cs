using Pharmase.Models;

namespace Pharmase.ViewModels
{
    public class HomeViewModel
    {
        public int TotalMedicines { get; set; }
        public decimal TodaySales { get; set; }
        public IEnumerable<Medicine> LowStockMedicines { get; set; }

        public IEnumerable<Sale> RecentSales { get; set; }  // Add this line

    }
}
