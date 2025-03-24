namespace Pharmase.ViewModels
{
    public class SaleViewModel
    {
        public DateTime SaleDate { get; set; } = DateTime.Now;
        public List<SaleItemViewModel> SaleItems { get; set; } = new List<SaleItemViewModel>();
    }
}
