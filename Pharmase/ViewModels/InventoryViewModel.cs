using Pharmase.Models;

namespace Pharmase.ViewModels
{
    public class InventoryViewModel
    {
        public IEnumerable<Medicine> medicines { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
