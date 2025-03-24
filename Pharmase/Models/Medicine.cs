using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pharmase.Models
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }


}
