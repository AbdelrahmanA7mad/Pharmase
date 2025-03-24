using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pharmase.Models
{
    public class SaleItem
    {

        public int Id { get; set; }  // معرف عنصر البيع

        public int MedicineId { get; set; }
        public Medicine? Medicine { get; set; }

        public int QuantitySold { get; set; }  // الكمية التي تم بيعها
        public decimal Price { get; set; }  // سعر الدواء وقت البيع

        public int SaleId { get; set; }  // معرف عملية البيع
        public Sale? Sale { get; set; }
    }

}
