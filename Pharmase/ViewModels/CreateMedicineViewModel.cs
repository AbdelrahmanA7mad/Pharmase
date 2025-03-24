using Microsoft.AspNetCore.Mvc.Rendering;
using Pharmase.Models;

namespace Pharmase.ViewModels
{
    public class CreateMedicineViewModel
    {
        public int ? Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime ExpireDate { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();

    }
}
