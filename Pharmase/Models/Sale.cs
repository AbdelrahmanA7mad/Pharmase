using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pharmase.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }  
        public DateTime SaleDate { get; set; } = DateTime.Now; 

        // العلاقة مع العناصر المباعة
        public ICollection<SaleItem>? SaleItems { get; set; }
    }

}
