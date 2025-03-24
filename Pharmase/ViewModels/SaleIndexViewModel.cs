using Pharmase.Models;

namespace Pharmase.ViewModels
{
    public class SaleIndexViewModel
    {

        public List<Sale> Sales { get; set; }
        public decimal TodaySales { get; set; }
        public int TotalOrders { get; set; }
    }
}
